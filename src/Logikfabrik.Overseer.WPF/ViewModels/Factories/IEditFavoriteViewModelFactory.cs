// <copyright file="IEditFavoriteViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System;

    /// <summary>
    /// The <see cref="IEditFavoriteViewModelFactory" /> interface.
    /// </summary>
    public interface IEditFavoriteViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <returns>A view model.</returns>
        EditFavoriteViewModel Create(Guid settingsId, string projectId);
    }
}
