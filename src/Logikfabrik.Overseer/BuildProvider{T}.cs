// <copyright file="BuildProvider{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="BuildProvider{T}" /> class. The base class for build providers.
    /// </summary>
    /// <typeparam name="T">The <see cref="ConnectionSettings" /> type.</typeparam>
    // ReSharper disable once InheritdocConsiderUsage
    public abstract class BuildProvider<T> : IBuildProvider
        where T : ConnectionSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProvider{T}" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        protected BuildProvider(T settings)
        {
            Ensure.That(settings).IsNotNull();

            Settings = settings;
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public T Settings { get; }

        /// <inheritdoc/>
        ConnectionSettings IBuildProvider.Settings => Settings;

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        public abstract Task<IEnumerable<IProject>> GetProjectsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the builds for the project with the specified project identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        public abstract Task<IEnumerable<IBuild>> GetBuildsAsync(string projectId, CancellationToken cancellationToken);

        /// <inheritdoc/>
        Task<IEnumerable<IProject>> IBuildProvider.GetProjectsAsync(CancellationToken cancellationToken)
        {
            return GetProjectsAsync(cancellationToken);
        }

        /// <inheritdoc/>
        Task<IEnumerable<IBuild>> IBuildProvider.GetBuildsAsync(string projectId, CancellationToken cancellationToken)
        {
            return GetBuildsAsync(projectId, cancellationToken);
        }
    }
}
