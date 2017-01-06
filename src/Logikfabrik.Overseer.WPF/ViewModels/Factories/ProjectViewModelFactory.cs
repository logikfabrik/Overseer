﻿// <copyright file="ProjectViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ProjectViewModelFactory" /> class.
    /// </summary>
    public class ProjectViewModelFactory : IProjectViewModelFactory
    {
        private readonly IBuildMonitor _buildMonitor;
        private readonly IBuildViewModelFactory _buildFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectViewModelFactory" /> class.
        /// </summary>
        /// <param name="buildMonitor">The build monitor.</param>
        /// <param name="buildFactory">The build factory.</param>
        public ProjectViewModelFactory(IBuildMonitor buildMonitor, IBuildViewModelFactory buildFactory)
        {
            Ensure.That(buildMonitor).IsNotNull();
            Ensure.That(buildFactory).IsNotNull();

            _buildMonitor = buildMonitor;
            _buildFactory = buildFactory;
        }

        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="project">The project identifier.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        public ProjectViewModel Create(Guid settingsId, IProject project)
        {
            Ensure.That(project).IsNotNull();

            return new ProjectViewModel(_buildMonitor, _buildFactory, settingsId, project.Id)
            {
                ProjectName = project.Name
            };
        }
    }
}