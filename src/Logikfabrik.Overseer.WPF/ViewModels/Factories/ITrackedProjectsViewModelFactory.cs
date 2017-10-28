// <copyright file="ITrackedProjectsViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="ITrackedProjectsViewModelFactory" /> interface.
    /// </summary>
    public interface ITrackedProjectsViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="projects">The projects.</param>
        /// <returns>A view model.</returns>
        TrackedProjectsViewModel Create(IEnumerable<TrackedProjectViewModel> projects);
    }
}