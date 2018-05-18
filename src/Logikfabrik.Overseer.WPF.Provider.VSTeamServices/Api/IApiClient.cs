// <copyright file="IApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Api
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The <see cref="ApiClient" /> class.
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="skip">The skip count.</param>
        /// <param name="take">The take count.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        Task<Models.Projects> GetProjectsAsync(int skip, int take, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the builds.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="skip">The skip count.</param>
        /// <param name="take">The take count.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        Task<Models.Builds> GetBuildsAsync(string projectId, int skip, int take, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the changes.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="buildId">The build identifier.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        Task<Models.Changes> GetChangesAsync(string projectId, string buildId, CancellationToken cancellationToken);
    }
}