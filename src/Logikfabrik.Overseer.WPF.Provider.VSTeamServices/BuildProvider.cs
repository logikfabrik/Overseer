// <copyright file="BuildProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EnsureThat;
    using Settings;
    using Settings.Extensions;

    /// <summary>
    /// The <see cref="BuildProvider" /> class.
    /// </summary>
    public class BuildProvider : Overseer.BuildProvider, IDisposable
    {
        private readonly Lazy<Api.ApiClient> _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProvider" /> class.
        /// </summary>
        /// <param name="buildProviderSettings">The build provider settings.</param>
        public BuildProvider(BuildProviderSettings buildProviderSettings)
            : base(buildProviderSettings)
        {
            _apiClient = new Lazy<Api.ApiClient>(() => GetApiClient(buildProviderSettings));
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>
        /// A task.
        /// </returns>
        public override async Task<IEnumerable<IProject>> GetProjectsAsync()
        {
            var projects = await _apiClient.Value.GetProjectsAsync(0, null).ConfigureAwait(false);

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

            var builds = new List<IBuild>();

            const int numberOfBuilds = 3;

            foreach (var build in (await _apiClient.Value.GetBuildsAsync(projectId, 0, numberOfBuilds).ConfigureAwait(false)).Value)
            {
                var changes = await _apiClient.Value.GetChangesAsync(projectId, build.Id).ConfigureAwait(false);

                builds.Add(new Build(build, changes.Value));
            }

            return builds;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
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

        private static Api.ApiClient GetApiClient(BuildProviderSettings buildProviderSettings)
        {
            var url = buildProviderSettings.GetSetting("Url");
            var token = buildProviderSettings.GetSetting("Token");

            return new Api.ApiClient(url, token);
        }
    }
}
