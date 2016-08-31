// <copyright file="BuildProviderSettingsStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    /// <summary>
    /// The <see cref="BuildProviderSettingsStore" /> class.
    /// </summary>
    public class BuildProviderSettingsStore : IBuildProviderSettingsStore
    {
        private readonly string _path;
        private readonly EventWaitHandle _handle;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderSettingsStore" /> class.
        /// </summary>
        public BuildProviderSettingsStore()
        {
            _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "settings.xml");
            _handle = new EventWaitHandle(true, EventResetMode.AutoReset, "b4908818-002e-42fb-a058-86ea4e47e36e");
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BuildProviderSettings>> LoadAsync()
        {
            return await Task.Run(() => Load());
        }

        /// <inheritdoc />
        public async void SaveAsync(IEnumerable<BuildProviderSettings> settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            await Task.Run(() => Save(settings));
        }

        private IEnumerable<BuildProviderSettings> Load()
        {
            if (!File.Exists(_path))
            {
                return new BuildProviderSettings[] { };
            }

            _handle.WaitOne();

            try
            {
                if (File.GetAttributes(_path).IsEncrypted())
                {
                    File.Decrypt(_path);
                }

                using (var reader = new StreamReader(_path))
                {
                    var serializer = new XmlSerializer(typeof(BuildProviderSettings[]));

                    return (BuildProviderSettings[])serializer.Deserialize(reader);
                }
            }
            finally
            {
                if (!File.GetAttributes(_path).IsEncrypted())
                {
                    File.Encrypt(_path);
                }

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
                using (var writer = new StreamWriter(_path, false))
                {
                    var serializer = new XmlSerializer(typeof (BuildProviderSettings[]));

                    serializer.Serialize(writer, settings.ToArray());
                }
            }
            catch (Exception ex)
            {
                var d = 0;
            }
            finally
            {
                //if (File.Exists(_path))
                //{
                //    File.Encrypt(_path);
                //}

                _handle.Set();
            }
        }
    }
}
