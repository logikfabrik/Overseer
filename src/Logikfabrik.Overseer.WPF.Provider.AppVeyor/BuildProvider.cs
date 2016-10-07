// <copyright file="BuildProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Settings;

    /// <summary>
    /// The <see cref="BuildProvider" /> class.
    /// </summary>
    public class BuildProvider : Overseer.BuildProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProvider" /> class.
        /// </summary>
        /// <param name="buildProviderSettings">The build provider settings.</param>
        public BuildProvider(BuildProviderSettings buildProviderSettings)
            : base(buildProviderSettings)
        {
        }

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        /// <value>
        /// The metadata.
        /// </value>
        public override IBuildProviderMetadata Metadata { get; } = new BuildProviderMetadata();

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>
        /// A task.
        /// </returns>
        public override async Task<IEnumerable<IProject>> GetProjectsAsync()
        {
            var apiClient = GetApiClient();

            var projects = await apiClient.GetProjectsAsync().ConfigureAwait(false);

            return projects.Select(project => new Project(project));
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
            var apiClient = GetApiClient();

            var projects = await apiClient.GetProjectsAsync().ConfigureAwait(false);

            var project = projects.SingleOrDefault(p => p.ProjectId == int.Parse(projectId));

            if (project == null)
            {
                return new IBuild[] { };
            }

            var projectHistory = await apiClient.GetProjectHistoryAsync(project.AccountName, project.Slug).ConfigureAwait(false);

            return projectHistory.Builds.Select(build => new Build(build));
        }

        private Api.ApiClient GetApiClient()
        {
            var token = BuildProviderSettings.GetSetting("Token");

            return new Api.ApiClient(token);
        }
    }
}
