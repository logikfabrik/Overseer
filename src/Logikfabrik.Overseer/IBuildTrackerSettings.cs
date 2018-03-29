// <copyright file="IBuildTrackerSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>
namespace Logikfabrik.Overseer
{
    /// <summary>
    /// The <see cref="IBuildTrackerSettings" /> interface.
    /// </summary>
    public interface IBuildTrackerSettings
    {
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
        /// Stores the build tracker settings.
        /// </summary>
        void Save();
    }
}