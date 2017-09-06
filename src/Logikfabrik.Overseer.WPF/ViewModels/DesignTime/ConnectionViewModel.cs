// <copyright file="ConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.DesignTime
{
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="ConnectionViewModel" /> class.
    /// </summary>
    public class ConnectionViewModel : IConnectionViewModel
    {
        public event EventHandler<ViewAttachedEventArgs> ViewAttached = (sender, e) => { };

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
        /// Gets a value indicating whether this instance is viewable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is viewable; otherwise, <c>false</c>.
        /// </value>
        public bool IsViewable { get; } = true;

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

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public object Parent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance should be kept alive on navigation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance should be kept alive on navigation; otherwise, <c>false</c>.
        /// </value>
        public bool KeepAlive { get; set; }

        /// <summary>
        /// Edits the connection.
        /// </summary>
        public void Edit()
        {
            // Method intentionally left empty.
        }

        /// <summary>
        /// Removes the connection.
        /// </summary>
        public void Remove()
        {
            // Method intentionally left empty.
        }

        /// <summary>
        /// Views the connection.
        /// </summary>
        public void View()
        {
            // Method intentionally left empty.
        }

        public void AttachView(object view, object context = null)
        {
            // Method intentionally left empty.
        }

        public void TryClose(bool? dialogResult = null)
        {
            // Method intentionally left empty.
        }

        public object GetView(object context = null)
        {
            throw new NotImplementedException();
        }
    }
}
