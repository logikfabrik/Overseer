// <copyright file="IApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.Codeship.Api
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The <see cref="IApiClient" /> interface.
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// Gets the organizations.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        Task<IEnumerable<Models.Organization>> GetOrganizationsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="perPage">Projects per page.</param>
        /// <param name="page">The current page.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        Task<Models.Projects> GetProjectsAsync(Guid organizationId, int perPage, int page, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the builds.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="perPage">Projects per page.</param>
        /// <param name="page">The current page.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        Task<Models.Builds> GetBuildsAsync(Guid organizationId, Guid projectId, int perPage, int page, CancellationToken cancellationToken);
    }
}