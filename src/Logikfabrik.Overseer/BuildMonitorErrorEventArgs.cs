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
        /// <param name="provider">The provider.</param>
        public BuildMonitorErrorEventArgs(IBuildProvider provider)
            : this(provider, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitorErrorEventArgs" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="project">The project.</param>
        public BuildMonitorErrorEventArgs(IBuildProvider provider, IProject project)
        {
            Provider = provider;
            Project = project;
        }

        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        public IBuildProvider Provider { get; }

        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>
        /// The project.
        /// </value>
        public IProject Project { get; }
    }
}
