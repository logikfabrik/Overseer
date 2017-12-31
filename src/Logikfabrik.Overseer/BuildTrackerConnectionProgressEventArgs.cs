// <copyright file="BuildTrackerConnectionProgressEventArgs.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildTrackerConnectionProgressEventArgs" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class BuildTrackerConnectionProgressEventArgs : BuildTrackerConnectionEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildTrackerConnectionProgressEventArgs" /> class.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="projects">The projects.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public BuildTrackerConnectionProgressEventArgs(Guid settingsId, IEnumerable<IProject> projects)
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
