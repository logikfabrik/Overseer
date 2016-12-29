// <copyright file="ConnectionSettingsStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System.Threading.Tasks;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionSettingsStore" /> class.
    /// </summary>
    public class ConnectionSettingsStore : IConnectionSettingsStore
    {
        private readonly IConnectionSettingsSerializer _serializer;
        private readonly IFileStore _fileStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsStore" /> class.
        /// </summary>
        /// <param name="serializer">The serializer.</param>
        /// <param name="fileStore">The file store.</param>
        public ConnectionSettingsStore(IConnectionSettingsSerializer serializer, IFileStore fileStore)
        {
            Ensure.That(serializer).IsNotNull();
            Ensure.That(fileStore).IsNotNull();

            _serializer = serializer;
            _fileStore = fileStore;
        }

        /// <summary>
        /// Loads the settings instance.
        /// </summary>
        /// <returns>
        /// The settings.
        /// </returns>
        public ConnectionSettings[] Load()
        {
            var contents = _fileStore.Read();

            return string.IsNullOrWhiteSpace(contents)
                ? new ConnectionSettings[] { }
                : _serializer.Deserialize(contents);
        }

        /// <summary>
        /// Saves the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// A task.
        /// </returns>
        public async Task SaveAsync(ConnectionSettings[] settings)
        {
            await Task.Run(() => Save(settings)).ConfigureAwait(false);
        }

        private void Save(ConnectionSettings[] settings)
        {
            Ensure.That(settings).IsNotNull();

            var fileContents = _serializer.Serialize(settings);

            _fileStore.Write(fileContents);
        }
    }
}
