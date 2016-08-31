// <copyright file="BuildProviderSettingsRepository.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The <see cref="BuildProviderSettingsRepository" /> class.
    /// </summary>
    public class BuildProviderSettingsRepository : IBuildProviderSettingsRepository
    {
        private readonly IBuildProviderSettingsStore _buildProviderSettingsStore;
        private readonly Lazy<IDictionary<Tuple<string, string>, BuildProviderSettings>> _currentSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderSettingsRepository" /> class.
        /// </summary>
        /// <param name="buildProviderSettingsStore">The build provider settings store.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="buildProviderSettingsStore" /> is <c>null</c>.</exception>
        public BuildProviderSettingsRepository(IBuildProviderSettingsStore buildProviderSettingsStore)
        {
            if (buildProviderSettingsStore == null)
            {
                throw new ArgumentNullException(nameof(buildProviderSettingsStore));
            }

            _buildProviderSettingsStore = buildProviderSettingsStore;
            _currentSettings = new Lazy<IDictionary<Tuple<string, string>, BuildProviderSettings>>(() => _buildProviderSettingsStore.LoadAsync().Result.ToDictionary(GetKey, setting => setting));
        }

        /// <summary>
        /// Gets settings.
        /// </summary>
        /// <returns>Settings.</returns>
        public IEnumerable<BuildProviderSettings> Get()
        {
            return _currentSettings.Value.Values;
        }

        /// <summary>
        /// Adds the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="settings" /> is <c>null</c>.</exception>
        public void Add(BuildProviderSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            _currentSettings.Value.Add(GetKey(settings), settings);

            _buildProviderSettingsStore.SaveAsync(_currentSettings.Value.Values);
        }

        private static Tuple<string, string> GetKey(BuildProviderSettings settings)
        {
            return new Tuple<string, string>(settings.Name, settings.ProviderTypeName);
        }
    }
}
