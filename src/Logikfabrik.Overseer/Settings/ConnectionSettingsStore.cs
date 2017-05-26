// <copyright file="ConnectionSettingsStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionSettingsStore" /> class.
    /// </summary>
    public class ConnectionSettingsStore : IConnectionSettingsStore
    {
        private readonly IConnectionSettingsSerializer _serializer;
        private readonly IConnectionSettingsEncrypter _encrypter;
        private readonly IFileStore _fileStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsStore" /> class.
        /// </summary>
        /// <param name="serializer">The serializer.</param>
        /// <param name="encrypter">The encrypter.</param>
        /// <param name="fileStore">The file store.</param>
        public ConnectionSettingsStore(IConnectionSettingsSerializer serializer, IConnectionSettingsEncrypter encrypter, IFileStore fileStore)
        {
            Ensure.That(serializer).IsNotNull();
            Ensure.That(encrypter).IsNotNull();
            Ensure.That(fileStore).IsNotNull();

            _serializer = serializer;
            _encrypter = encrypter;
            _fileStore = fileStore;
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        /// <returns>
        /// The settings.
        /// </returns>
        public ConnectionSettings[] Load()
        {
            var xml = _fileStore.Read();

            return string.IsNullOrWhiteSpace(xml)
                ? new ConnectionSettings[] { }
                : _serializer.Deserialize(_encrypter.Decrypt(xml));
        }

        /// <summary>
        /// Saves the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Save(ConnectionSettings[] settings)
        {
            Ensure.That(settings).IsNotNull();

            var xml = _encrypter.Encrypt(_serializer.Serialize(settings));

            _fileStore.Write(xml);
        }
    }
}
