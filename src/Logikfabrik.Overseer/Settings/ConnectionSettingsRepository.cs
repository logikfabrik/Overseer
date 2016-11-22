﻿// <copyright file="ConnectionSettingsRepository.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionSettingsRepository" /> class.
    /// </summary>
    public class ConnectionSettingsRepository : IConnectionSettingsRepository
    {
        private readonly IConnectionSettingsStore _settingsStore;
        private readonly Lazy<IDictionary<Guid, ConnectionSettings>> _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsRepository" /> class.
        /// </summary>
        /// <param name="settingsStore">The settings store.</param>
        public ConnectionSettingsRepository(IConnectionSettingsStore settingsStore)
        {
            Ensure.That(settingsStore).IsNotNull();

            _settingsStore = settingsStore;
            _settings = new Lazy<IDictionary<Guid, ConnectionSettings>>(() =>
            {
                return _settingsStore.LoadAsync().Result.ToDictionary(buildProviderSettings => buildProviderSettings.Id, buildProviderSettings => buildProviderSettings);
            });
        }

        /// <summary>
        /// Adds the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Add(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            var clone = settings.Clone();

            _settings.Value.Add(clone.Id, clone);

            Save();
        }

        /// <summary>
        /// Removes the settings with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Remove(Guid id)
        {
            Ensure.That(id).IsNotEmpty();

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
        public void Update(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            if (!_settings.Value.ContainsKey(settings.Id))
            {
                return;
            }

            var clone = settings.Clone();

            _settings.Value[clone.Id] = clone;

            Save();
        }

        /// <summary>
        /// Gets the settings with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The settings with the specified identifier.
        /// </returns>
        public ConnectionSettings Get(Guid id)
        {
            Ensure.That(id).IsNotEmpty();

            ConnectionSettings settings;

            return _settings.Value.TryGetValue(id, out settings) ? settings.Clone() : null;
        }

        /// <summary>
        /// Gets all the settings.
        /// </summary>
        /// <returns>
        /// All the settings.
        /// </returns>
        public IEnumerable<ConnectionSettings> GetAll()
        {
            return _settings.Value.Values.Select(settings => settings.Clone());
        }

        private void Save()
        {
            _settingsStore.SaveAsync(_settings.Value.Values.ToArray());
        }
    }
}
