// <copyright file="BuildProviderSettingsStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System.Threading.Tasks;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildProviderSettingsStore" /> class.
    /// </summary>
    public class BuildProviderSettingsStore : IBuildProviderSettingsStore
    {
        private readonly IBuildProviderSettingsSerializer _serializer;
        private readonly IFileStore _fileStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderSettingsStore" /> class.
        /// </summary>
        /// <param name="serializer">The serializer.</param>
        /// <param name="fileStore">The file store.</param>
        public BuildProviderSettingsStore(IBuildProviderSettingsSerializer serializer, IFileStore fileStore)
        {
            Ensure.That(serializer).IsNotNull();
            Ensure.That(fileStore).IsNotNull();

            _serializer = serializer;
            _fileStore = fileStore;
        }

        public async Task<BuildProviderSettings[]> LoadAsync()
        {
            return await Task.Run(() => Load()).ConfigureAwait(false);
        }

        public async Task SaveAsync(BuildProviderSettings[] settings)
        {
            await Task.Run(() => Save(settings)).ConfigureAwait(false);
        }

        private BuildProviderSettings[] Load()
        {
            var fileContents = _fileStore.Read();

            return string.IsNullOrWhiteSpace(fileContents)
                ? new BuildProviderSettings[] { }
                : _serializer.Deserialize(fileContents);
        }

        private void Save(BuildProviderSettings[] settings)
        {
            Ensure.That(settings).IsNotNull();

            var fileContents = _serializer.Serialize(settings);

            _fileStore.Write(fileContents);
        }
    }
}
