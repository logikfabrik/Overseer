﻿// <copyright file="ConnectionSettingsViewModel{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Caliburn.Micro;
    using FluentValidation;
    using Settings;

    /// <summary>
    /// The <see cref="ConnectionSettingsViewModel{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The <see cref="ConnectionSettings" /> type.</typeparam>
    public abstract class ConnectionSettingsViewModel<T> : PropertyChangedBase, IDataErrorInfo
        where T : ConnectionSettings
    {
        private bool _isDirty;
        private string _name;
        private IEnumerable<ProjectToMonitorViewModel> _projectsToMonitor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsViewModel{T}" /> class.
        /// </summary>
        protected ConnectionSettingsViewModel()
        {
            _isDirty = true;
            _projectsToMonitor = new ProjectToMonitorViewModel[] { };
        }

        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <value>
        /// The validator.
        /// </value>
        public abstract IValidator Validator { get; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        /// <summary>
        /// Gets or sets the projects to monitor.
        /// </summary>
        /// <value>
        /// The projects to monitor.
        /// </value>
        public IEnumerable<ProjectToMonitorViewModel> ProjectsToMonitor
        {
            get
            {
                return _projectsToMonitor;
            }

            set
            {
                _projectsToMonitor = value;
                NotifyOfPropertyChange(() => ProjectsToMonitor);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is dirty. Dirty settings are settings yet to be tried.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is dirty; otherwise, <c>false</c>.
        /// </value>
        public bool IsDirty
        {
            get
            {
                return _isDirty;
            }

            set
            {
                _isDirty = value;
                NotifyOfPropertyChange(() => IsDirty);
                NotifyOfPropertyChange(() => IsNotDirty);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is not dirty. Dirty settings are settings yet to be tried.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is not dirty; otherwise, <c>false</c>.
        /// </value>
        public bool IsNotDirty => !IsDirty;

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        public string Error => null;

        /// <summary>
        /// Gets the error message for the property with the specified name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <returns>
        /// The error message, if any.
        /// </returns>
        public string this[string name]
        {
            get
            {
                var result = Validator.Validate(this);

                if (result.IsValid)
                {
                    return null;
                }

                var error = result.Errors.FirstOrDefault(e => e.PropertyName == name);

                return error?.ErrorMessage;
            }
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns>The settings.</returns>
        public abstract T GetSettings();

        /// <summary>
        /// Updates the settings.
        /// </summary>
        /// <param name="current">The current settings.</param>
        public abstract void UpdateSettings(T current);
    }
}