// <copyright file="BuildMonitorConnectionErrorEventArgs.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="BuildMonitorConnectionErrorEventArgs" /> class.
    /// </summary>
    public class BuildMonitorConnectionErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitorConnectionErrorEventArgs" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public BuildMonitorConnectionErrorEventArgs(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            Settings = settings;
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public ConnectionSettings Settings { get; }
    }
}