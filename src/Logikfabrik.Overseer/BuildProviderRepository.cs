// <copyright file="BuildProviderRepository.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="BuildProviderRepository" /> class.
    /// </summary>
    public class BuildProviderRepository : IBuildProviderRepository
    {
        private readonly IConnectionSettingsRepository _settingsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderRepository" /> class.
        /// </summary>
        /// <param name="settingsRepository">The settings repository.</param>
        public BuildProviderRepository(IConnectionSettingsRepository settingsRepository)
        {
            Ensure.That(settingsRepository).IsNotNull();

            _settingsRepository = settingsRepository;
        }

        /// <summary>
        /// Gets all the providers.
        /// </summary>
        /// <returns>
        /// All the providers.
        /// </returns>
        public IEnumerable<IBuildProvider> GetAll()
        {
            var settings = _settingsRepository.GetAll();

            return settings.Select(GetProvider);
        }

        private static IBuildProvider GetProvider(ConnectionSettings settings)
        {
            var constructor = settings.ProviderType.GetConstructor(new[] { settings.GetType() });

            // ReSharper disable once PossibleNullReferenceException
            var provider = (IBuildProvider)constructor.Invoke(new object[] { settings });

            return provider;
        }
    }
}