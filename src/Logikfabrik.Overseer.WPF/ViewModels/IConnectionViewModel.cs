// <copyright file="IConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// The <see cref="IConnectionViewModel" /> interface.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
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
        /// Gets a value indicating whether this instance is viewable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is viewable; otherwise, <c>false</c>.
        /// </value>
        bool IsViewable { get; }

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
        /// Gets the filtered projects.
        /// </summary>
        /// <value>
        /// The filtered projects.
        /// </value>
        ICollectionView FilteredProjects { get; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        string Filter { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has projects.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has projects; otherwise, <c>false</c>.
        /// </value>
        bool HasProjects { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has no projects.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has no projects; otherwise, <c>false</c>.
        /// </value>
        bool HasNoProjects { get; }

        /// <summary>
        /// Edits this instance.
        /// </summary>
        void Edit();

        /// <summary>
        /// Removes this instance.
        /// </summary>
        void Remove();

        /// <summary>
        /// Views this instance.
        /// </summary>
        void View();
    }
}