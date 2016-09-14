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
        private readonly IBuildProviderSettingsRepository _buildProviderSettingsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderRepository" /> class.
        /// </summary>
        /// <param name="buildProviderSettingsRepository">The build provider settings repository.</param>
        public BuildProviderRepository(IBuildProviderSettingsRepository buildProviderSettingsRepository)
        {
            Ensure.That(buildProviderSettingsRepository).IsNotNull();

            _buildProviderSettingsRepository = buildProviderSettingsRepository;
        }

        /// <summary>
        /// Gets the providers.
        /// </summary>
        /// <returns>The providers.</returns>
        public IEnumerable<BuildProvider> GetProviders()
        {
            var settings = _buildProviderSettingsRepository.Get();

            var providers = settings.Select(BuildProviderFactory.GetProvider);

            return providers;
        }
    }
}