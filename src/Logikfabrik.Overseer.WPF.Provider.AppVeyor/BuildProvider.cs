// <copyright file="BuildProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor
{
    using System.Collections.Generic;
    using System.Linq;

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
        public override string Name { get; } = "AppVeyor";

        public override IEnumerable<IProject> GetProjects()
        {
            var token = BuildProviderSettings.GetSetting("Token");

            var apiClient = new Api.ApiClient(token);

            var apiProjects = apiClient.GetProjects().Result;

            return apiProjects.Select(p => new Project(p));
        }

        public override IEnumerable<IBuild> GetBuilds(string projectId)
        {
            var token = BuildProviderSettings.GetSetting("Token");

            var apiClient = new Api.ApiClient(token);

            var apiProjects = apiClient.GetProjects().Result;

            var apiProject = apiProjects.FirstOrDefault(p => p.ProjectId == int.Parse(projectId));

            if (apiProject == null)
            {
                return new IBuild[] {};
            }

            var apiProjectHistory = apiClient.GetProjectHistory(apiProject.AccountName, apiProject.Slug).Result;

            return apiProjectHistory.Builds.Select(b => new Build(b));
        }
    }
}
