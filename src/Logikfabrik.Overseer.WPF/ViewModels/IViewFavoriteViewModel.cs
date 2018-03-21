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

        /// <summary>
        /// Gets or sets the project name.
        /// </summary>
        /// <value>
        /// The project name.
        /// </value>
        string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        bool IsBusy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is errored.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is errored; otherwise, <c>false</c>.
        /// </value>
        bool IsErrored { get; set; }
    }
}
