// <copyright file="BuildMonitorConnectionEventArgs.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildMonitorConnectionEventArgs" /> class.
    /// </summary>
    public abstract class BuildMonitorConnectionEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitorConnectionEventArgs" /> class.
        /// </summary>
        /// <param name="settingsId">The settings ID.</param>
        protected BuildMonitorConnectionEventArgs(Guid settingsId)
        {
            Ensure.That(settingsId).IsNotEmpty();

            SettingsId = settingsId;
        }

        /// <summary>
        /// Gets the settings ID.
        /// </summary>
        /// <value>
        /// The settings ID.
        /// </value>
        public Guid SettingsId { get; }
    }
}
