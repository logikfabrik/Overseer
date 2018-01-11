// <copyright file="IEditTrackedProjectsViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="IEditTrackedProjectsViewModelFactory" /> interface.
    /// </summary>
    public interface IEditTrackedProjectsViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="editTrackedProjectViewModels">The edit tracked project view models.</param>
        /// <returns>A view model.</returns>
        EditTrackedProjectsViewModel Create(IEnumerable<EditTrackedProjectViewModel> editTrackedProjectViewModels);
    }
}