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
        /// Adds the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        void Add(BuildProviderSettings settings);

        /// <summary>
        /// Gets settings.
        /// </summary>
        /// <returns>Settings.</returns>
        IEnumerable<BuildProviderSettings> Get();
    }
}