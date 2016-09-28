﻿// <copyright file="IBuildProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Settings;

    /// <summary>
    /// The <see cref="IBuildProvider" /> interface.
    /// </summary>
    public interface IBuildProvider
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets or sets the build provider settings.
        /// </summary>
        /// <value>
        /// The build provider settings.
        /// </value>
        BuildProviderSettings BuildProviderSettings { get; set; }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>A task.</returns>
        Task<IEnumerable<IProject>> GetProjectsAsync();

        /// <summary>
        /// Gets the builds for the project with the specified project identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns>A task.</returns>
        Task<IEnumerable<IBuild>> GetBuildsAsync(string projectId);
    }
}