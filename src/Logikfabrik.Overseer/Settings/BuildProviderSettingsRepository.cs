// <copyright file="BuildProviderSettingsRepository.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildProviderSettingsRepository" /> class.
    /// </summary>
    public class BuildProviderSettingsRepository : IBuildProviderSettingsRepository
    {
        private readonly IBuildProviderSettingsStore _buildProviderSettingsStore;
        private readonly Lazy<IDictionary<Guid, BuildProviderSettings>> _currentBuildProviderSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderSettingsRepository" /> class.
        /// </summary>
        /// <param name="buildProviderSettingsStore">The build provider settings store.</param>
        public BuildProviderSettingsRepository(IBuildProviderSettingsStore buildProviderSettingsStore)
        {
            Ensure.That(buildProviderSettingsStore).IsNotNull();

            _buildProviderSettingsStore = buildProviderSettingsStore;
            _currentBuildProviderSettings = new Lazy<IDictionary<Guid, BuildProviderSettings>>(() =>
            {
                return _buildProviderSettingsStore.LoadAsync().Result.ToDictionary(buildProviderSettings => buildProviderSettings.Id, buildProviderSettings => buildProviderSettings);
            });
        }

        /// <summary>
        /// Gets the build provider settings.
        /// </summary>
        /// <returns>
        /// The build provider settings.
        /// </returns>
        public IEnumerable<BuildProviderSettings> Get()
        {
            return _currentBuildProviderSettings.Value.Values;
        }

        /// <summary>
        /// Adds the specified build provider settings.
        /// </summary>
        /// <param name="buildProviderSettings">The build provider settings.</param>
        public void Add(BuildProviderSettings buildProviderSettings)
        {
            Ensure.That(buildProviderSettings).IsNotNull();

            _currentBuildProviderSettings.Value.Add(buildProviderSettings.Id, buildProviderSettings);

            _buildProviderSettingsStore.SaveAsync(_currentBuildProviderSettings.Value.Values);
        }
    }
}
