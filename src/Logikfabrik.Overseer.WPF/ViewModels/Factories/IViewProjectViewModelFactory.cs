// <copyright file="IViewProjectViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System;

    /// <summary>
    /// The <see cref="IViewProjectViewModelFactory" /> interface.
    /// </summary>
    public interface IViewProjectViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <returns>A view model.</returns>
        ViewProjectViewModel Create(Guid settingsId, string projectId);

        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="projectName">The project name.</param>
        /// <returns>A view model.</returns>
        ViewProjectViewModel Create(Guid settingsId, string projectId, string projectName);
    }
}
