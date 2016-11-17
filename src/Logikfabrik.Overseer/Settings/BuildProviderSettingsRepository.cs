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
        private readonly Lazy<IDictionary<Guid, BuildProviderSettings>> _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderSettingsRepository" /> class.
        /// </summary>
        /// <param name="buildProviderSettingsStore">The build provider settings store.</param>
        public BuildProviderSettingsRepository(IBuildProviderSettingsStore buildProviderSettingsStore)
        {
            Ensure.That(buildProviderSettingsStore).IsNotNull();

            _buildProviderSettingsStore = buildProviderSettingsStore;
            _settings = new Lazy<IDictionary<Guid, BuildProviderSettings>>(() =>
            {
                return _buildProviderSettingsStore.LoadAsync().Result.ToDictionary(buildProviderSettings => buildProviderSettings.Id, buildProviderSettings => buildProviderSettings);
            });
        }

        /// <summary>
        /// Adds the specified settings.
        /// </summary>
        /// <param name="settings">The build provider settings.</param>
        public void Add(BuildProviderSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            _settings.Value.Add(settings.Id, settings);

            Save();
        }

        /// <summary>
        /// Removes the settings with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Remove(Guid id)
        {
            if (!_settings.Value.ContainsKey(id))
            {
                return;
            }

            _settings.Value.Remove(id);

            Save();
        }

        /// <summary>
        /// Updates the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Update(BuildProviderSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            if (!_settings.Value.ContainsKey(settings.Id))
            {
                return;
            }

            _settings.Value[settings.Id] = settings;

            Save();
        }

        /// <summary>
        /// Gets all the settings.
        /// </summary>
        /// <returns>
        /// All the settings.
        /// </returns>
        public IEnumerable<BuildProviderSettings> GetAll()
        {
            return _settings.Value.Values;
        }

        private void Save()
        {
            _buildProviderSettingsStore.SaveAsync(_settings.Value.Values);
        }
    }
}
