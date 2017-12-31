// <copyright file="BuildTrackerProjectProgressEventArgs.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildTrackerProjectProgressEventArgs" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class BuildTrackerProjectProgressEventArgs : BuildTrackerProjectEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildTrackerProjectProgressEventArgs" /> class.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="project">The project.</param>
        /// <param name="builds">The builds.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public BuildTrackerProjectProgressEventArgs(Guid settingsId, IProject project, IEnumerable<IBuild> builds)
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