// <copyright file="BuildProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices
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
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        public override async Task<IEnumerable<IProject>> GetProjectsAsync(CancellationToken cancellationToken)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            var projects = await _apiClient.Value.GetProjectsAsync(0, null, cancellationToken).ConfigureAwait(false);

            return projects.Value.Select(project => new Project(project));
        }

        /// <summary>
        /// Gets the builds for the project with the specified project identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        public override async Task<IEnumerable<IBuild>> GetBuildsAsync(string projectId, CancellationToken cancellationToken)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            var builds = new List<IBuild>();

            const int numberOfBuilds = 3;

            foreach (var build in (await _apiClient.Value.GetBuildsAsync(projectId, 0, numberOfBuilds, cancellationToken).ConfigureAwait(false)).Value)
            {
                var changes = await _apiClient.Value.GetChangesAsync(projectId, build.Id, cancellationToken).ConfigureAwait(false);

                builds.Add(new Build(build, changes.Value));
            }

            return builds;
        }

        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            // ReSharper disable once InvertIf
            if (disposing)
            {
                if (!_apiClient.IsValueCreated)
                {
                    return;
                }

                _apiClient.Value.Dispose();
            }

            _isDisposed = true;
        }

        private static Api.ApiClient GetApiClient(ConnectionSettings settings)
        {
            return new Api.ApiClient(settings.Url, settings.Token);
        }
    }
}