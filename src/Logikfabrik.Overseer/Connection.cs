// <copyright file="Connection.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using EnsureThat;
    using Extensions;
    using Settings;

    /// <summary>
    /// The <see cref="Connection" /> class.
    /// </summary>
    public class Connection : IDisposable
    {
        private readonly IBuildProviderStrategy _buildProviderStrategy;
        private IBuildProvider _provider;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection" /> class.
        /// </summary>
        /// <param name="buildProviderStrategy">The build provider strategy.</param>
        /// <param name="settings">The settings.</param>
        public Connection(IBuildProviderStrategy buildProviderStrategy, ConnectionSettings settings)
        {
            Ensure.That(buildProviderStrategy).IsNotNull();
            Ensure.That(settings).IsNotNull();

            _buildProviderStrategy = buildProviderStrategy;
            Settings = settings.Clone();
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public ConnectionSettings Settings { get; }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        public async Task<IEnumerable<IProject>> GetProjectsAsync(CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed(_isDisposed);

            return await GetProvider().GetProjectsAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the builds for the specified project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        public async Task<IEnumerable<IBuild>> GetBuildsAsync(IProject project, CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(project).IsNotNull();

            return await GetProvider().GetBuildsAsync(project.Id, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                _provider = null;
            }

            _isDisposed = true;
        }

        private IBuildProvider GetProvider()
        {
            return _provider ?? (_provider = _buildProviderStrategy.Create(Settings));
        }
    }
}