// <copyright file="ConnectionSettingsViewModel{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Caliburn.Micro;
    using EnsureThat;
    using Factories;
    using FluentValidation;
    using Settings;

    /// <summary>
    /// The <see cref="ConnectionSettingsViewModel{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The <see cref="ConnectionSettings" /> type.</typeparam>
    public abstract class ConnectionSettingsViewModel<T> : PropertyChangedBase, IDataErrorInfo
        where T : ConnectionSettings
    {
        private readonly IProjectToMonitorViewModelFactory _projectToMonitorFactory;
        private string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsViewModel{T}" /> class.
        /// </summary>
        /// <param name="projectToMonitorFactory">The project to monitor factory.</param>
        protected ConnectionSettingsViewModel(IProjectToMonitorViewModelFactory projectToMonitorFactory)
        {
            Ensure.That(projectToMonitorFactory).IsNotNull();

            _projectToMonitorFactory = projectToMonitorFactory;
            ProjectsToMonitor = new ObservableCollection<ProjectToMonitorViewModel>();
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
        /// Gets the projects to monitor.
        /// </summary>
        /// <value>
        /// The projects to monitor.
        /// </value>
        public ObservableCollection<ProjectToMonitorViewModel> ProjectsToMonitor { get; private set; }

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
        /// Gets the updated settings.
        /// </summary>
        /// <param name="current">The current settings.</param>
        /// <returns>The updated settings.</returns>
        public abstract T GetUpdatedSettings(T current);

        // TODO: Should be protected?
        public async Task GetProjectsAsync()
        {
            if (!Validator.Validate(this).IsValid)
            {
                return;
            }

            var settings = GetSettings();

            using (var provider = BuildProviderFactory.GetProvider(settings))
            {
                var projects = await provider.GetProjectsAsync(CancellationToken.None).ConfigureAwait(false);

                // TODO: Known issue; settings.ProjectsToMonitor is null/empty. Needs to be loaded.
                ProjectsToMonitor = new ObservableCollection<ProjectToMonitorViewModel>(projects.Select(project => _projectToMonitorFactory.Create(project, settings.ProjectsToMonitor.Contains(project.Id))));

                NotifyOfPropertyChange(() => ProjectsToMonitor);
            }
        }
    }
}