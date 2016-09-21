﻿// <copyright file="BuildProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

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
        private const int TakeCount = 10;
        private const int SkipCount = 0;

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

            var projects = await apiClient.GetProjectsAsync(SkipCount, TakeCount).ConfigureAwait(false);

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

            var builds = await apiClient.GetBuildsAsync(Guid.Parse(projectId), SkipCount, TakeCount).ConfigureAwait(false);

            return builds.Value.Select(build => new Build(build));
        }

        private Api.ApiClient GetApiClient()
        {
            var url = BuildProviderSettings.GetSetting("Url");
            var token = BuildProviderSettings.GetSetting("Token");

            return new Api.ApiClient(url, token);
        }
    }
}
