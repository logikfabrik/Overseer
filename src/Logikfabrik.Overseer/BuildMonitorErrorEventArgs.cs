// <copyright file="BuildMonitorErrorEventArgs.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;

    /// <summary>
    /// The <see cref="BuildMonitorErrorEventArgs" /> class.
    /// </summary>
    public class BuildMonitorErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitorErrorEventArgs" /> class.
        /// </summary>
        public BuildMonitorErrorEventArgs()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitorErrorEventArgs" /> class.
        /// </summary>
        /// <param name="buildProvider">The build provider.</param>
        public BuildMonitorErrorEventArgs(IBuildProvider buildProvider)
            : this(buildProvider, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitorErrorEventArgs" /> class.
        /// </summary>
        /// <param name="buildProvider">The build provider.</param>
        /// <param name="project">The project.</param>
        public BuildMonitorErrorEventArgs(IBuildProvider buildProvider, IProject project)
        {
            BuildProvider = buildProvider;
            Project = project;
        }

        /// <summary>
        /// Gets the build provider.
        /// </summary>
        /// <value>
        /// The build provider.
        /// </value>
        public IBuildProvider BuildProvider { get; }

        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>
        /// The project.
        /// </value>
        public IProject Project { get; }
    }
}
