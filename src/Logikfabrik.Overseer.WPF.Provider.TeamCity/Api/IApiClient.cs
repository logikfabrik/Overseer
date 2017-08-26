// <copyright file="IApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Api
{
    using System.Threading;
    using System.Threading.Tasks;
    using Caching;

    /// <summary>
    /// The <see cref="IApiClient" /> interface.
    /// </summary>
    public interface IApiClient : ICacheable
    {
        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        Task<Models.Projects> GetProjectsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the build types.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        Task<Models.BuildTypes> GetBuildTypesAsync(CancellationToken cancellationToken);
    }
}