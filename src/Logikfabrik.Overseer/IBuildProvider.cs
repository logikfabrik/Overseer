// <copyright file="IBuildProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Settings;

    /// <summary>
    /// The <see cref="IBuildProvider" /> interface.
    /// </summary>
    public interface IBuildProvider : IDisposable
    {
        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        ConnectionSettings Settings { get; }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        Task<IEnumerable<IProject>> GetProjectsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the builds for the project with the specified project identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        Task<IEnumerable<IBuild>> GetBuildsAsync(string projectId, CancellationToken cancellationToken);
    }
}
