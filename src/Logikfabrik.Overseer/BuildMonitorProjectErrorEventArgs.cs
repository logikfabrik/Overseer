// <copyright file="BuildMonitorProjectErrorEventArgs.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="BuildMonitorProjectErrorEventArgs" /> class.
    /// </summary>
    public class BuildMonitorProjectErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitorProjectErrorEventArgs" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="project">The project.</param>
        public BuildMonitorProjectErrorEventArgs(ConnectionSettings settings, IProject project)
        {
            Ensure.That(settings).IsNotNull();
            Ensure.That(project).IsNotNull();

            Settings = settings;
            Project = project;
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public ConnectionSettings Settings { get; }

        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>
        /// The project.
        /// </value>
        public IProject Project { get; }
    }
}
