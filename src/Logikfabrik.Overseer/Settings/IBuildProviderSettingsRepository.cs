// <copyright file="IBuildProviderSettingsRepository.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="IBuildProviderSettingsRepository" /> interface.
    /// </summary>
    public interface IBuildProviderSettingsRepository
    {
        /// <summary>
        /// Adds the specified build provider settings.
        /// </summary>
        /// <param name="buildProviderSettings">The build provider settings.</param>
        void AddBuildProviderSettings(BuildProviderSettings buildProviderSettings);

        /// <summary>
        /// Gets the build provider settings.
        /// </summary>
        /// <returns>The build provider settings.</returns>
        IEnumerable<BuildProviderSettings> GetBuildProviderSettings();
    }
}