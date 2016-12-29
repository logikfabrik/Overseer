// <copyright file="BuildProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// The <see cref="BuildProvider" /> class.
    /// </summary>
    public class BuildProvider : BuildProvider<ConnectionSettings>
    {
        private readonly Lazy<Api.ApiClient> _apiClient;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProvider" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public BuildProvider(ConnectionSettings settings)
            : base(settings)
        {
            _apiClient = new Lazy<Api.ApiClient>(() => GetApiClient(settings));
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>
        /// A task.
        /// </returns>
        public override async Task<IEnumerable<IProject>> GetProjectsAsync()
        {
            var projects = await _apiClient.Value.GetProjectsAsync().ConfigureAwait(false);

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
            var projects = await _apiClient.Value.GetProjectsAsync().ConfigureAwait(false);

            var project = projects.SingleOrDefault(p => p.ProjectId == int.Parse(projectId));

            if (project == null)
            {
                return new IBuild[] { };
            }

            const int numberOfBuilds = 3;

            var projectHistory = await _apiClient.Value.GetProjectHistoryAsync(project.AccountName, project.Slug, numberOfBuilds).ConfigureAwait(false);

            return projectHistory.Builds.Select(build => new Build(build));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // ReSharper disable once InvertIf
            if (disposing)
            {
                if (!_apiClient.IsValueCreated)
                {
                    return;
                }

                _apiClient.Value.Dispose();
            }
        }

        private static Api.ApiClient GetApiClient(ConnectionSettings settings)
        {
            return new Api.ApiClient(settings.Token);
        }
    }
}
