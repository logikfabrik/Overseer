// <copyright file="BuildMonitorConnectionProgressEventArgs.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="BuildMonitorConnectionProgressEventArgs" /> class.
    /// </summary>
    public class BuildMonitorConnectionProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitorConnectionProgressEventArgs" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="projects">The projects.</param>
        public BuildMonitorConnectionProgressEventArgs(ConnectionSettings settings, IEnumerable<IProject> projects)
        {
            Ensure.That(settings).IsNotNull();
            Ensure.That(projects).IsNotNull();

            Settings = settings;
            Projects = projects;
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public ConnectionSettings Settings { get; }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public IEnumerable<IProject> Projects { get; }
    }
}
