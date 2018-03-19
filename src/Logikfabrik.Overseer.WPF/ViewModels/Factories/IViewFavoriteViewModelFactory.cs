// <copyright file="IViewFavoriteViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System;

    /// <summary>
    /// The <see cref="IViewFavoriteViewModelFactory" /> interface.
    /// </summary>
    public interface IViewFavoriteViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <returns>A view model.</returns>
        ViewFavoriteViewModel Create(Guid settingsId, string projectId);
    }
}
