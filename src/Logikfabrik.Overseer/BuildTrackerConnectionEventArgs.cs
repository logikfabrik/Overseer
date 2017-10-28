// <copyright file="BuildTrackerConnectionEventArgs.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildTrackerConnectionEventArgs" /> class.
    /// </summary>
    public abstract class BuildTrackerConnectionEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildTrackerConnectionEventArgs" /> class.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        protected BuildTrackerConnectionEventArgs(Guid settingsId)
        {
            Ensure.That(settingsId).IsNotEmpty();

            SettingsId = settingsId;
        }

        /// <summary>
        /// Gets the settings identifier.
        /// </summary>
        /// <value>
        /// The settings identifier.
        /// </value>
        public Guid SettingsId { get; }
    }
}
