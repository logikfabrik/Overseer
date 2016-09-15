// <copyright file="BuildMonitorProgressEventArgs.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildMonitorProgressEventArgs" /> class.
    /// </summary>
    public class BuildMonitorProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitorProgressEventArgs" /> class.
        /// </summary>
        /// <param name="percentProgress">The percent progress.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="project">The project.</param>
        /// <param name="builds">The builds.</param>
        public BuildMonitorProgressEventArgs(int percentProgress, BuildProvider provider, IProject project, IEnumerable<IBuild> builds)
        {
            Ensure.That(percentProgress).IsInRange(0, 100);
            Ensure.That(provider).IsNotNull();
            Ensure.That(project).IsNotNull();
            Ensure.That(builds).IsNotNull();

            PercentProgress = percentProgress;
            Provider = provider;
            Project = project;
            Builds = builds;
        }

        /// <summary>
        /// Gets the percent progress.
        /// </summary>
        /// <value>
        /// The percent progress.
        /// </value>
        public int PercentProgress { get; }

        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        public BuildProvider Provider { get; }

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