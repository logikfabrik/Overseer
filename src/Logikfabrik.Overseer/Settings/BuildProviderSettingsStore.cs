// <copyright file="BuildProviderSettingsStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildProviderSettingsStore" /> class.
    /// </summary>
    public class BuildProviderSettingsStore : IBuildProviderSettingsStore
    {
        private const string FileWaitHandleName = "b4908818-002e-42fb-a058-86ea4e47e36e";

        private readonly string _filePath;
        private readonly EventWaitHandle _fileEventWaitHandle;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderSettingsStore" /> class.
        /// </summary>
        public BuildProviderSettingsStore()
        {
            _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GetProduct(), "Providers.xml");
            _fileEventWaitHandle = new EventWaitHandle(true, EventResetMode.AutoReset, FileWaitHandleName);
        }

        /// <summary>
        /// Loads the build provider settings.
        /// </summary>
        /// <returns>
        /// A task.
        /// </returns>
        public async Task<IEnumerable<BuildProviderSettings>> LoadAsync()
        {
            return await Task.Run(() => Load()).ConfigureAwait(false);
        }

        /// <summary>
        /// Saves the specified build provider settings.
        /// </summary>
        /// <param name="buildProviderSettings">The build provider settings.</param>
        /// <returns>
        /// A task.
        /// </returns>
        public async Task SaveAsync(IEnumerable<BuildProviderSettings> buildProviderSettings)
        {
            Ensure.That(buildProviderSettings).IsNotNull();

            await Task.Run(() => Save(buildProviderSettings)).ConfigureAwait(false);
        }

        private static string GetProduct()
        {
            var attribute = Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyProductAttribute), false) as AssemblyProductAttribute;

            return attribute?.Product;
        }

        private IEnumerable<BuildProviderSettings> Load()
        {
            _fileEventWaitHandle.WaitOne();

            try
            {
                if (!File.Exists(_filePath))
                {
                    return new BuildProviderSettings[] { };
                }

                using (var reader = new StreamReader(_filePath))
                {
                    var serializer = XmlSerializer.FromTypes(new[] { typeof(BuildProviderSettings[]) })[0];

                    return (BuildProviderSettings[])serializer.Deserialize(reader);
                }
            }
            finally
            {
                _fileEventWaitHandle.Set();
            }
        }

        private void Save(IEnumerable<BuildProviderSettings> buildProviderSettings)
        {
            Ensure.That(buildProviderSettings).IsNotNull();

            _fileEventWaitHandle.WaitOne();

            try
            {
                var directoryPath = Path.GetDirectoryName(_filePath);

                if (string.IsNullOrWhiteSpace(directoryPath))
                {
                    throw new Exception();
                }

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (var writer = new StreamWriter(_filePath, false))
                {
                    var serializer = XmlSerializer.FromTypes(new[] { typeof(BuildProviderSettings[]) })[0];

                    serializer.Serialize(writer, buildProviderSettings.ToArray());
                }
            }
            finally
            {
                if (File.Exists(_filePath) && !File.GetAttributes(_filePath).HasFlag(FileAttributes.Encrypted))
                {
                    File.Encrypt(_filePath);
                }

                _fileEventWaitHandle.Set();
            }
        }
    }
}
