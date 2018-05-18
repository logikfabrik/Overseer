// <copyright file="IApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The <see cref="IApiClient" /> interface.
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// Gets the repositories.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        Task<Models.Repositories> GetRepositoriesAsync(int limit, int offset, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the builds.
        /// </summary>
        /// <param name="repositoryId">The repository identifier.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        Task<Models.Builds> GetBuildsAsync(string repositoryId, int limit, int offset, CancellationToken cancellationToken);
    }
}
