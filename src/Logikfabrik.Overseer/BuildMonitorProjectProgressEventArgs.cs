// <copyright file="BuildMonitorProjectProgressEventArgs.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildMonitorProjectProgressEventArgs" /> class.
    /// </summary>
    public class BuildMonitorProjectProgressEventArgs : BuildMonitorProjectEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitorProjectProgressEventArgs" /> class.
        /// </summary>
        /// <param name="settingsId">The settings ID.</param>
        /// <param name="project">The project.</param>
        /// <param name="builds">The builds.</param>
        public BuildMonitorProjectProgressEventArgs(Guid settingsId, IProject project, IEnumerable<IBuild> builds)
            : base(settingsId, project)
        {
            Ensure.That(builds).IsNotNull();

            Builds = builds;
        }

        /// <summary>
        /// Gets the builds.
        /// </summary>
        /// <value>
        /// The builds.
        /// </value>
        public IEnumerable<IBuild> Builds { get; }
    }
}