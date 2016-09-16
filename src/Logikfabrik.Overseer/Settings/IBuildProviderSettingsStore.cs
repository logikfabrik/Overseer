// <copyright file="IBuildProviderSettingsStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// The <see cref="IBuildProviderSettingsStore" /> interface.
    /// </summary>
    public interface IBuildProviderSettingsStore
    {
        /// <summary>
        /// Saves the specified build provider settings.
        /// </summary>
        /// <param name="buildProviderSettings">The build provider settings.</param>
        /// <returns>A task.</returns>
        Task SaveAsync(IEnumerable<BuildProviderSettings> buildProviderSettings);

        /// <summary>
        /// Loads the build provider settings.
        /// </summary>
        /// <returns>A task.</returns>
        Task<IEnumerable<BuildProviderSettings>> LoadAsync();
    }
}