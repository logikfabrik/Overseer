// <copyright file="IEditTrackedProjectViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    /// <summary>
    /// The <see cref="IEditTrackedProjectViewModelFactory" /> interface.
    /// </summary>
    public interface IEditTrackedProjectViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="projectName">The project name.</param>
        /// <param name="track">Whether the specified project should be tracked.</param>
        /// <returns>A view model.</returns>
        EditTrackedProjectViewModel Create(string projectId, string projectName, bool track);
    }
}