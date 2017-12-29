// <copyright file="BuildProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

using System.Linq;

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildProvider" /> class.
    /// </summary>
    public class BuildProvider : BuildProvider<ConnectionSettings>
    {
        private readonly Api.IApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProvider" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="apiClient">The API client.</param>
        public BuildProvider(ConnectionSettings settings, Api.IApiClient apiClient)
            : base(settings)
        {
            Ensure.That(apiClient).IsNotNull();

            _apiClient = apiClient;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<IProject>> GetProjectsAsync(CancellationToken cancellationToken)
        {
            var repositories = await _apiClient.GetRepositoriesAsync(100, 0, cancellationToken).ConfigureAwait(false);

            return repositories.Records.Select(repository => new Project(repository));
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<IBuild>> GetBuildsAsync(string projectId, CancellationToken cancellationToken)
        {
            return new IBuild[] {};
        }
    }
}
