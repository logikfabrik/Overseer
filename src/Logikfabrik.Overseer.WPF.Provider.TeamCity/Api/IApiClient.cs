// <copyright file="IApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Api
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The <see cref="IApiClient" /> interface.
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        Task<Models.Projects> GetProjectsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the builds.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="count">The count.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task.</returns>
        Task<Models.Builds> GetBuildsAsync(string projectId, int count, CancellationToken cancellationToken);
    }
}