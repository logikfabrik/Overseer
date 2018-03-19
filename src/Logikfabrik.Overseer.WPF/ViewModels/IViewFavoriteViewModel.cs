// <copyright file="IViewFavoriteViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;

    /// <summary>
    /// The <see cref="IViewFavoriteViewModel" /> interface.
    /// </summary>
    public interface IViewFavoriteViewModel
    {
        /// <summary>
        /// Gets the settings identifier.
        /// </summary>
        /// <value>
        /// The settings identifier.
        /// </value>
        Guid SettingsId { get; }

        /// <summary>
        /// Gets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        string ProjectId { get; }
    }
}
