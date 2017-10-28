// <copyright file="ITrackedProjectViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    /// <summary>
    /// The <see cref="ITrackedProjectViewModelFactory" /> interface.
    /// </summary>
    public interface ITrackedProjectViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="projectName">The project name.</param>
        /// <param name="track">Whether the specified project should be tracked.</param>
        /// <returns>A view model.</returns>
        TrackedProjectViewModel Create(string projectId, string projectName, bool track);
    }
}