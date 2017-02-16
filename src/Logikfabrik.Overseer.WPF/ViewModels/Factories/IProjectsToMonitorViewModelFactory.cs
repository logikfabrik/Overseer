// <copyright file="IProjectsToMonitorViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="IProjectsToMonitorViewModelFactory" /> interface.
    /// </summary>
    public interface IProjectsToMonitorViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="projects">The projects.</param>
        /// <returns>A view model.</returns>
        ProjectsToMonitorViewModel Create(IEnumerable<ProjectToMonitorViewModel> projects);
    }
}