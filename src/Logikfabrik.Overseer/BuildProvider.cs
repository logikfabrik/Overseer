// <copyright file="BuildProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="BuildProvider" /> class.
    /// </summary>
    public abstract class BuildProvider : IBuildProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProvider" /> class.
        /// </summary>
        /// <param name="buildProviderSettings">The build provider settings.</param>
        protected BuildProvider(BuildProviderSettings buildProviderSettings)
        {
            Ensure.That(buildProviderSettings).IsNotNull();

            BuildProviderSettings = buildProviderSettings;
        }

        /// <summary>
        /// Gets the build provider settings.
        /// </summary>
        /// <value>
        /// The build provider settings.
        /// </value>
        public BuildProviderSettings BuildProviderSettings { get; }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>A task.</returns>
        public abstract Task<IEnumerable<IProject>> GetProjectsAsync();

        /// <summary>
        /// Gets the builds for the project with the specified project identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns>A task.</returns>
        public abstract Task<IEnumerable<IBuild>> GetBuildsAsync(string projectId);
    }
}
