// <copyright file="ConnectionSettingsExtensions.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings.Extensions
{
    /// <summary>
    /// The <see cref="ConnectionSettingsExtensions" /> class.
    /// </summary>
    public static class ConnectionSettingsExtensions
    {
        /// <summary>
        /// Gets a signature for the specified <see cref="ConnectionSettings" /> instance.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>A signature.</returns>
        public static int Signature(this ConnectionSettings settings)
        {
            return new ConnectionSettingsSignature(settings).Signature;
        }
    }
}
