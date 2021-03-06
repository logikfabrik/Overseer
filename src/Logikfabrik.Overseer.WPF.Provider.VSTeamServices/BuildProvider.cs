﻿// <copyright file="BuildProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildProvider" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class BuildProvider : BuildProvider<ConnectionSettings>
    {
        private readonly Api.IApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProvider" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="apiClient">The API client.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public BuildProvider(ConnectionSettings settings, Api.IApiClient apiClient)
            : base(settings)
        {
            Ensure.That(apiClient).IsNotNull();

            _apiClient = apiClient;
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<IProject>> GetProjectsAsync(CancellationToken cancellationToken)
        {
            var projects = await _apiClient.GetProjectsAsync(0, int.MaxValue, cancellationToken).ConfigureAwait(false);

            return projects.Value.Select(project => new Project(project)).ToArray();
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<IBuild>> GetBuildsAsync(string projectId, CancellationToken cancellationToken)
        {
            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            var builds = new List<IBuild>();

            foreach (var build in (await _apiClient.GetBuildsAsync(projectId, 0, Settings.BuildsPerProject, cancellationToken).ConfigureAwait(false)).Value)
            {
                var changes = await _apiClient.GetChangesAsync(projectId, build.Id, cancellationToken).ConfigureAwait(false);

                builds.Add(new Build(build, changes.Value));
            }

            return builds.ToArray();
        }
    }
}