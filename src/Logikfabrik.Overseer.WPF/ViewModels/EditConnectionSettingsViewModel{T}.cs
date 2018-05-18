// <copyright file="EditConnectionSettingsViewModel{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.ComponentModel;
    using System.Linq;
    using Caliburn.Micro;
    using FluentValidation;
    using Settings;

    /// <summary>
    /// The <see cref="EditConnectionSettingsViewModel{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The <see cref="ConnectionSettings" /> type.</typeparam>
    // ReSharper disable once InheritdocConsiderUsage
    public abstract class EditConnectionSettingsViewModel<T> : PropertyChangedBase, IDataErrorInfo
        where T : ConnectionSettings
    {
        private bool _isDirty;
        private string _name;
        private int _buildsPerProject;
        private EditTrackedProjectsViewModel _trackedProjects;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionSettingsViewModel{T}" /> class.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        protected EditConnectionSettingsViewModel()
        {
            _isDirty = true;
        }

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
                NotifyOfPropertyChange(() => IsValid);
            }
        }

        /// <summary>
        /// Gets or sets the tracked projects.
        /// </summary>
        /// <value>
        /// The tracked projects.
        /// </value>
        public EditTrackedProjectsViewModel TrackedProjects
        {
            get
            {
                return _trackedProjects;
            }

            set
            {
                _trackedProjects = value;
                NotifyOfPropertyChange(() => TrackedProjects);
            }
        }

        /// <summary>
        /// Gets or sets the number of builds per project.
        /// </summary>
        /// <value>
        /// The number of builds per project.
        /// </value>
        public int BuildsPerProject
        {
            get
            {
                return _buildsPerProject;
            }

            set
            {
                _buildsPerProject = value;
                NotifyOfPropertyChange(() => BuildsPerProject);
                NotifyOfPropertyChange(() => IsValid);
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
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid => Validator.Validate(this).IsValid;

        /// <inheritdoc />
        public string Error => null;

        /// <summary>
        /// Sets the validator.
        /// </summary>
        protected IValidator Validator { private get; set; }

        /// <inheritdoc />
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