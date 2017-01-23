// <copyright file="ProjectToMonitorViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using EnsureThat;

    /// <summary>
    /// The <see cref="ProjectToMonitorViewModelFactory" /> class.
    /// </summary>
    public class ProjectToMonitorViewModelFactory : IProjectToMonitorViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="monitor">Whether the specified project should be monitored.</param>
        /// <returns>A view model.</returns>
        public ProjectToMonitorViewModel Create(IProject project, bool monitor)
        {
            Ensure.That(project).IsNotNull();

            return new ProjectToMonitorViewModel(project.Name, project.Id, monitor);
        }
    }
}
