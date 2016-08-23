﻿// <copyright file="BuildProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="BuildProvider" /> interface.
    /// </summary>
    public abstract class BuildProvider
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>The projects.</returns>
        public abstract IEnumerable<IProject> GetProjects();

        /// <summary>
        /// Gets the builds for the project with the specified project identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns>The builds for the project with the specified project identifier.</returns>
        public abstract IEnumerable<IBuild> GetBuilds(string projectId);
    }
}
