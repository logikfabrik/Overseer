// <copyright file="ProjectsToMonitorViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="ProjectsToMonitorViewModelFactory" /> class.
    /// </summary>
    public class ProjectsToMonitorViewModelFactory : IProjectsToMonitorViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="projects">The projects.</param>
        /// <returns>A view model.</returns>
        public ProjectsToMonitorViewModel Create(IEnumerable<ProjectToMonitorViewModel> projects)
        {
            return new ProjectsToMonitorViewModel(projects);
        }
    }
}
