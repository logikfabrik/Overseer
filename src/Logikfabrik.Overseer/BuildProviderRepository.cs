// <copyright file="BuildProviderRepository.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="BuildProviderRepository" /> class.
    /// </summary>
    public class BuildProviderRepository : IBuildProviderRepository
    {
        private readonly Lazy<IDictionary<Guid, IBuildProvider>> _currentProviders;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderRepository" /> class.
        /// </summary>
        /// <param name="settingsRepository">The settings repository.</param>
        public BuildProviderRepository(IConnectionSettingsRepository settingsRepository)
        {
            Ensure.That(settingsRepository).IsNotNull();

            _currentProviders = new Lazy<IDictionary<Guid, IBuildProvider>>(() =>
            {
                return settingsRepository.GetAll().ToDictionary(buildProviderSettings => buildProviderSettings.Id, GetProvider);
            });
        }

        /// <summary>
        /// Gets the providers.
        /// </summary>
        /// <returns>
        /// The providers.
        /// </returns>
        public IEnumerable<IBuildProvider> GetProviders()
        {
            // TODO: Refresh the collection if settings are added, removed, or changed.
            return _currentProviders.Value.Values;
        }

        private static IBuildProvider GetProvider(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            var constructor = settings.ProviderType.GetConstructor(new[] { settings.GetType() });

            // ReSharper disable once PossibleNullReferenceException
            var provider = (IBuildProvider)constructor.Invoke(new object[] { settings });

            return provider;
        }
    }
}