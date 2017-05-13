// <copyright file="IApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI.Api
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    /// <summary>
    /// The <see cref="IApiClient" /> interface.
    /// </summary>
    public interface IApiClient : IDisposable
    {
        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        Task<IEnumerable<Project>> GetProjectsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the build types.
        /// </summary>
        /// <param name="projectVcsType">The project VCS type.</param>
        /// <param name="projectUsername">The project username.</param>
        /// <param name="projectName">The project name.</param>
        /// <param name="offset">The offset count.</param>
        /// <param name="limit">The limit count.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        Task<IEnumerable<Build>> GetBuildsAsync(string projectVcsType, string projectUsername, string projectName, int offset, int limit, CancellationToken cancellationToken);
    }
}
