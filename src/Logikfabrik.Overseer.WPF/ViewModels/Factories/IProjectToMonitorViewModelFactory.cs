// <copyright file="IProjectToMonitorViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    /// <summary>
    /// The <see cref="IProjectToMonitorViewModelFactory" /> interface.
    /// </summary>
    public interface IProjectToMonitorViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="monitor">Whether the specified project should be monitored.</param>
        /// <returns>A view model.</returns>
        ProjectToMonitorViewModel Create(IProject project, bool monitor);
    }
}