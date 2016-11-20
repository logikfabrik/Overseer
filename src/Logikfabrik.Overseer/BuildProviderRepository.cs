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
        private readonly Lazy<IDictionary<Guid, IBuildProvider>> _currentBuildProviders;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderRepository" /> class.
        /// </summary>
        /// <param name="buildProviderSettingsRepository">The build provider settings repository.</param>
        public BuildProviderRepository(IBuildProviderSettingsRepository buildProviderSettingsRepository)
        {
            Ensure.That(buildProviderSettingsRepository).IsNotNull();

            _currentBuildProviders = new Lazy<IDictionary<Guid, IBuildProvider>>(() =>
            {
                return buildProviderSettingsRepository.GetAll().ToDictionary(buildProviderSettings => buildProviderSettings.Id, GetBuildProvider);
            });
        }

        /// <summary>
        /// Gets the build providers.
        /// </summary>
        /// <returns>The build providers.</returns>
        public IEnumerable<IBuildProvider> GetBuildProviders()
        {
            // TODO: Refresh the collection if settings are added, removed, or changed.
            return _currentBuildProviders.Value.Values;
        }

        private static IBuildProvider GetBuildProvider(BuildProviderSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            var constructor = settings.BuildProviderType.GetConstructor(new[] { typeof(BuildProviderSettings) });

            // ReSharper disable once PossibleNullReferenceException
            var buildProvider = (IBuildProvider)constructor.Invoke(new object[] { settings });

            return buildProvider;
        }
    }
}