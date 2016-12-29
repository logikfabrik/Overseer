// <copyright file="BuildMonitorProjectProgressEventArgs.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="BuildMonitorProjectProgressEventArgs" /> class.
    /// </summary>
    public class BuildMonitorProjectProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitorProjectProgressEventArgs" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="project">The project.</param>
        /// <param name="builds">The builds.</param>
        public BuildMonitorProjectProgressEventArgs(ConnectionSettings settings, IProject project, IEnumerable<IBuild> builds)
        {
            Ensure.That(settings).IsNotNull();
            Ensure.That(project).IsNotNull();
            Ensure.That(builds).IsNotNull();

            Settings = settings;
            Project = project;
            Builds = builds;
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

        /// <summary>
        /// Gets the builds.
        /// </summary>
        /// <value>
        /// The builds.
        /// </value>
        public IEnumerable<IBuild> Builds { get; }
    }
}