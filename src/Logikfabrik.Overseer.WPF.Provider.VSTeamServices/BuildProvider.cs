// <copyright file="BuildProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

using Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Api.Models;

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildProvider" /> class.
    /// </summary>
    public class BuildProvider : Overseer.BuildProvider
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public override string Name { get; } = "VS Team Services";

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>
        /// A task.
        /// </returns>
        public override async Task<IEnumerable<IProject>> GetProjectsAsync()
        {
            var apiClient = GetApiClient();

            var projects = await apiClient.GetProjectsAsync(0, 10).ConfigureAwait(false);

            return projects.Value.Select(project => new Project(project));
        }

        /// <summary>
        /// Gets the builds for the project with the specified project identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns>
        /// A task.
        /// </returns>
        public override async Task<IEnumerable<IBuild>> GetBuildsAsync(string projectId)
        {
            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            var apiClient = GetApiClient();

            var builds = new List<IBuild>();

            foreach (var build in (await apiClient.GetBuildsAsync(Guid.Parse(projectId), 0, 10).ConfigureAwait(false)).Value)
            {
                string comment = null;

                switch (build.Repository.Type)
                {
                    case RepositoryType.Tfs:
                        var changesets = await apiClient.GetChangesetsAsync(build.SourceVersion, 0, 1).ConfigureAwait(false);

                        comment = changesets.Value.FirstOrDefault()?.Comment;

                        break;

                    case RepositoryType.TfsGit:
                        comment = "Git"; // TODO: Get git commit.

                        break;
                }

                builds.Add(new Build(build, comment));
            }

            return builds;
        }

        private Api.ApiClient GetApiClient()
        {
            var url = BuildProviderSettings.GetSetting("Url");
            var token = BuildProviderSettings.GetSetting("Token");

            return new Api.ApiClient(url, token);
        }
    }
}
