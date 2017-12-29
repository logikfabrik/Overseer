// <copyright file="IApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
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
        Task<Models.Repositories> GetRepositoriesAsync(int limit, int offset, CancellationToken cancellationToken);
    }
}
