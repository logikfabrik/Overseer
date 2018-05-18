// <copyright file="BuildTrackerProjectEventArgs.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildTrackerProjectEventArgs" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public abstract class BuildTrackerProjectEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildTrackerProjectEventArgs" /> class.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="project">The project.</param>
        // ReSharper disable once InheritdocConsiderUsage
        protected BuildTrackerProjectEventArgs(Guid settingsId, IProject project)
        {
            Ensure.That(settingsId).IsNotEmpty();
            Ensure.That(project).IsNotNull();

            SettingsId = settingsId;
            Project = project;
        }

        /// <summary>
        /// Gets the settings identifier.
        /// </summary>
        /// <value>
        /// The settings identifier.
        /// </value>
        public Guid SettingsId { get; }

        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>
        /// The project.
        /// </value>
        public IProject Project { get; }
    }
}
