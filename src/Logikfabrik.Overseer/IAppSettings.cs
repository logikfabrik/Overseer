// <copyright file="IAppSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    /// <summary>
    /// The <see cref="IAppSettings" /> interface.
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        /// Gets or sets the culture name.
        /// </summary>
        /// <value>
        /// The culture name.
        /// </value>
        string CultureName { get; set; }

        /// <summary>
        /// Gets the expiration.
        /// </summary>
        /// <value>
        /// The expiration.
        /// </value>
        int Expiration { get; }

        /// <summary>
        /// Gets or sets the interval in seconds.
        /// </summary>
        /// <value>
        /// The interval in seconds.
        /// </value>
        int Interval { get; set; }

        /// <summary>
        /// Stores the current values of the application settings properties.
        /// </summary>
        void Save();
    }
}