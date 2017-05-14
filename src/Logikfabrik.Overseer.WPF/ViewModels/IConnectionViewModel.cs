// <copyright file="IConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="IConnectionViewModel" /> interface.
    /// </summary>
    public interface IConnectionViewModel : IViewModel
    {
        /// <summary>
        /// Gets the settings identifier.
        /// </summary>
        /// <value>
        /// The settings identifier.
        /// </value>
        Guid SettingsId { get; }

        /// <summary>
        /// Gets or sets the settings name.
        /// </summary>
        /// <value>
        /// The settings name.
        /// </value>
        string SettingsName { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        bool IsBusy { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is editable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is editable; otherwise, <c>false</c>.
        /// </value>
        bool IsEditable { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is errored.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is errored; otherwise, <c>false</c>.
        /// </value>
        bool IsErrored { get; }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        IEnumerable<IProjectViewModel> Projects { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has projects.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has projects; otherwise, <c>false</c>.
        /// </value>
        bool HasProjects { get; }

        /// <summary>
        /// Edit the connection.
        /// </summary>
        void Edit();

        /// <summary>
        /// Remove the connection.
        /// </summary>
        void Remove();

        /// <summary>
        /// View the connection.
        /// </summary>
        void View();
    }
}