// <copyright file="BuildProviderSettingsExtensions.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings.Extensions
{
    using System.Linq;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildProviderSettingsExtensions" /> class.
    /// </summary>
    public static class BuildProviderSettingsExtensions
    {
        /// <summary>
        /// Gets the setting with the specified name.
        /// </summary>
        /// <param name="buildProviderSettings">The build provider settings.</param>
        /// <param name="name">The name.</param>
        /// <returns>The setting.</returns>
        public static string GetSetting(this BuildProviderSettings buildProviderSettings, string name)
        {
            Ensure.That(name).IsNotNullOrWhiteSpace();

            return buildProviderSettings.Settings.FirstOrDefault(setting => setting.Name == name)?.Value;
        }
    }
}
