// <copyright file="BuildMonitorConnectionProgressEventArgs.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildMonitorConnectionProgressEventArgs" /> class.
    /// </summary>
    public class BuildMonitorConnectionProgressEventArgs : BuildMonitorConnectionEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitorConnectionProgressEventArgs" /> class.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="projects">The projects.</param>
        public BuildMonitorConnectionProgressEventArgs(Guid settingsId, IEnumerable<IProject> projects)
            : base(settingsId)
        {
            Ensure.That(projects).IsNotNull();

            Projects = projects;
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public IEnumerable<IProject> Projects { get; }
    }
}
