// <copyright file="BuildMonitorState.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System.Collections.Generic;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildMonitorState" /> class.
    /// </summary>
    public class BuildMonitorState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitorState" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="project">The project.</param>
        /// <param name="builds">The builds.</param>
        public BuildMonitorState(BuildProvider provider, IProject project, IEnumerable<IBuild> builds)
        {
            Ensure.That(provider).IsNotNull();
            Ensure.That(project).IsNotNull();
            Ensure.That(builds).IsNotNull();

            Provider = provider;
            Project = project;
            Builds = builds;
        }

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
