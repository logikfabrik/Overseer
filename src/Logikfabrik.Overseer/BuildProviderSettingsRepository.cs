// <copyright file="BuildProviderSettingsRepository.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The <see cref="BuildProviderSettingsRepository" /> class.
    /// </summary>
    public class BuildProviderSettingsRepository : IBuildProviderSettingsRepository
    {
        private readonly IBuildProviderSettingsStore _store;
        private readonly Lazy<IDictionary<string, BuildProviderSettings>> _currentSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderSettingsRepository" /> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="store" /> is <c>null</c>.</exception>
        public BuildProviderSettingsRepository(IBuildProviderSettingsStore store)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }

            _store = store;
            _currentSettings = new Lazy<IDictionary<string, BuildProviderSettings>>(() =>
            {
                return _store.LoadAsync().Result.ToDictionary(setting => setting.Name, setting => setting);
            });
        }

        /// <inheritdoc />
        public IEnumerable<BuildProviderSettings> Get()
        {
            return _currentSettings.Value.Values;
        }

        /// <inheritdoc />
        public void Add(BuildProviderSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            _currentSettings.Value.Add(settings.Name, settings);

            _store.SaveAsync(_currentSettings.Value.Values);
        }
    }
}
