// <copyright file="BuildProvider{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="BuildProvider{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The <see cref="ConnectionSettings" /> type.</typeparam>
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

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        ConnectionSettings IBuildProvider.Settings => Settings;

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>
        /// A task.
        /// </returns>
        public abstract Task<IEnumerable<IProject>> GetProjectsAsync();

        /// <summary>
        /// Gets the builds for the project with the specified project identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns>
        /// A task.
        /// </returns>
        public abstract Task<IEnumerable<IBuild>> GetBuildsAsync(string projectId);

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>
        /// A task.
        /// </returns>
        Task<IEnumerable<IProject>> IBuildProvider.GetProjectsAsync()
        {
            return GetProjectsAsync();
        }

        /// <summary>
        /// Gets the builds for the project with the specified project identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns>
        /// A task.
        /// </returns>
        Task<IEnumerable<IBuild>> IBuildProvider.GetBuildsAsync(string projectId)
        {
            return GetBuildsAsync(projectId);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public abstract void Dispose();
    }
}
