// <copyright file="BuildProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.Codeship
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        /// <inheritdoc />
        public override async Task<IEnumerable<IProject>> GetProjectsAsync(CancellationToken cancellationToken)
        {
            var organizations = await _apiClient.GetOrganizationsAsync(cancellationToken).ConfigureAwait(false);

            var projects = new List<Api.Models.Project>();

            foreach (var organization in organizations)
            {
                var page = 1;

                Api.Models.Projects pageable;

                do
                {
                    pageable = await _apiClient.GetProjectsAsync(organization.Id, 50, page, cancellationToken).ConfigureAwait(false);

                    projects.AddRange(pageable.Items);

                    page++;
                }
                while (!IsLastPage(pageable));
            }

            return projects.Select(project => new Project(project)).ToArray();
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<IBuild>> GetBuildsAsync(string projectId, CancellationToken cancellationToken)
        {
            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            var id = Guid.Parse(projectId);

            var organizations = await _apiClient.GetOrganizationsAsync(cancellationToken).ConfigureAwait(false);

            foreach (var organization in organizations)
            {
                var page = 1;

                Api.Models.Projects pageable;

                do
                {
                    pageable = await _apiClient.GetProjectsAsync(organization.Id, 50, page, cancellationToken).ConfigureAwait(false);

                    var project = pageable.Items.FirstOrDefault(p => p.Id == id);

                    if (project != null)
                    {
                        var builds = await _apiClient.GetBuildsAsync(organization.Id, project.Id, Settings.BuildsPerProject, 1, cancellationToken).ConfigureAwait(false);

                        return builds.Items.Select(build => new Build(build)).ToArray();
                    }

                    page++;
                }
                while (!IsLastPage(pageable));
            }

            return new IBuild[] { };
        }

        private static bool IsLastPage(Api.Models.IPageable pageable)
        {
            return pageable.Page * pageable.PerPage >= pageable.Total;
        }
    }
}
