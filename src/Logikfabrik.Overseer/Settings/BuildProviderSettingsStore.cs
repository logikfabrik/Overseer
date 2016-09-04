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

    /// <summary>
    /// The <see cref="BuildProviderSettingsStore" /> class.
    /// </summary>
    public class BuildProviderSettingsStore : IBuildProviderSettingsStore
    {
        private const string HandleName = "b4908818-002e-42fb-a058-86ea4e47e36e";

        private readonly string _path;
        private readonly EventWaitHandle _handle;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderSettingsStore" /> class.
        /// </summary>
        public BuildProviderSettingsStore()
        {
            _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GetProduct(), "Providers.xml");
            _handle = new EventWaitHandle(true, EventResetMode.AutoReset, HandleName);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BuildProviderSettings>> LoadAsync()
        {
            return await Task.Run(() => Load()).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task SaveAsync(IEnumerable<BuildProviderSettings> settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            await Task.Run(() => Save(settings)).ConfigureAwait(false);
        }

        private static string GetProduct()
        {
            var attribute = Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyProductAttribute), false) as AssemblyProductAttribute;

            return attribute?.Product;
        }

        private IEnumerable<BuildProviderSettings> Load()
        {
            _handle.WaitOne();

            try
            {
                if (!File.Exists(_path))
                {
                    return new BuildProviderSettings[] { };
                }

                using (var reader = new StreamReader(_path))
                {
                    var serializer = XmlSerializer.FromTypes(new[] { typeof(BuildProviderSettings[]) })[0];

                    return (BuildProviderSettings[])serializer.Deserialize(reader);
                }
            }
            finally
            {
                _handle.Set();
            }
        }

        private void Save(IEnumerable<BuildProviderSettings> settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            _handle.WaitOne();

            try
            {
                var directoryPath = Path.GetDirectoryName(_path);

                if (string.IsNullOrWhiteSpace(directoryPath))
                {
                    throw new Exception();
                }

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (var writer = new StreamWriter(_path, false))
                {
                    var serializer = XmlSerializer.FromTypes(new[] { typeof(BuildProviderSettings[]) })[0];

                    serializer.Serialize(writer, settings.ToArray());
                }
            }
            finally
            {
                if (File.Exists(_path) && !File.GetAttributes(_path).HasFlag(FileAttributes.Encrypted))
                {
                    File.Encrypt(_path);
                }

                _handle.Set();
            }
        }
    }
}
