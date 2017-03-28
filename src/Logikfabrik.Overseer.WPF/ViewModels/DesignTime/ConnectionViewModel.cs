// <copyright file="ConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.DesignTime
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="ConnectionViewModel" /> class.
    /// </summary>
    public class ConnectionViewModel : IConnectionViewModel
    {
        /// <summary>
        /// Gets the settings identifier.
        /// </summary>
        /// <value>
        /// The settings identifier.
        /// </value>
        public Guid SettingsId { get; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the settings name.
        /// </summary>
        /// <value>
        /// The settings name.
        /// </value>
        public string SettingsName { get; set; } = "My Connection";

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        public bool IsBusy { get; } = false;

        /// <summary>
        /// Gets a value indicating whether this instance is editable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is editable; otherwise, <c>false</c>.
        /// </value>
        public bool IsEditable { get; } = true;

        /// <summary>
        /// Gets a value indicating whether this instance is errored.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is errored; otherwise, <c>false</c>.
        /// </value>
        public bool IsErrored { get; } = false;

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public IEnumerable<IProjectViewModel> Projects { get; } = new[] { new ProjectViewModel(), new ProjectViewModel(), new ProjectViewModel() };

        /// <summary>
        /// Gets a value indicating whether this instance has projects.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has projects; otherwise, <c>false</c>.
        /// </value>
        public bool HasProjects { get; } = true;

        public int? ProjectsCount { get; } = 3;

        /// <summary>
        /// Edit the connection.
        /// </summary>
        public void Edit()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove the connection.
        /// </summary>
        public void Remove()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// View the connection.
        /// </summary>
        public void View()
        {
            throw new NotImplementedException();
        }
    }
}
