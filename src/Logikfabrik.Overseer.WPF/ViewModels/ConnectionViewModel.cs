// <copyright file="ConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using Caliburn.Micro;
    using EnsureThat;
    using Factories;

    /// <summary>
    /// The <see cref="ConnectionViewModel" /> class.
    /// </summary>
    public abstract class ConnectionViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IProjectViewModelFactory _projectFactory;
        private readonly IRemoveConnectionViewModelFactory _removeConnectionFactory;
        private readonly List<ProjectViewModel> _projects;
        private string _settingsName;
        private bool _isBusy;
        private bool _isErrored;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildMonitor">The build monitor.</param>
        /// <param name="projectFactory">The project factory.</param>
        /// <param name="removeConnectionFactory">The remove connection factory.</param>
        /// <param name="settingsId">The settings identifier.</param>
        protected ConnectionViewModel(IEventAggregator eventAggregator, IBuildMonitor buildMonitor, IProjectViewModelFactory projectFactory, IRemoveConnectionViewModelFactory removeConnectionFactory, Guid settingsId)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildMonitor).IsNotNull();
            Ensure.That(projectFactory).IsNotNull();
            Ensure.That(removeConnectionFactory).IsNotNull();
            Ensure.That(settingsId).IsNotEmpty();

            _eventAggregator = eventAggregator;
            _projectFactory = projectFactory;
            _removeConnectionFactory = removeConnectionFactory;
            SettingsId = settingsId;
            _isBusy = true;
            _isErrored = false;
            _projects = new List<ProjectViewModel>();

            WeakEventManager<IBuildMonitor, BuildMonitorConnectionErrorEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.ConnectionError), BuildMonitorConnectionError);
            WeakEventManager<IBuildMonitor, BuildMonitorConnectionProgressEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.ConnectionProgressChanged), BuildMonitorConnectionProgressChanged);
        }

        /// <summary>
        /// Gets the settings identifier.
        /// </summary>
        /// <value>
        /// The settings identifier.
        /// </value>
        public Guid SettingsId { get; }

        /// <summary>
        /// Gets or sets the settings name.
        /// </summary>
        /// <value>
        /// The settings name.
        /// </value>
        public string SettingsName
        {
            get
            {
                return _settingsName;
            }

            set
            {
                _settingsName = value;
                NotifyOfPropertyChange(() => SettingsName);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            private set
            {
                _isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
                NotifyOfPropertyChange(() => IsEditable);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is editable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is editable; otherwise, <c>false</c>.
        /// </value>
        public bool IsEditable => IsErrored || !IsBusy;

        /// <summary>
        /// Gets a value indicating whether this instance is errored.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is errored; otherwise, <c>false</c>.
        /// </value>
        public bool IsErrored
        {
            get
            {
                return _isErrored;
            }

            private set
            {
                _isErrored = value;
                NotifyOfPropertyChange(() => IsErrored);
                NotifyOfPropertyChange(() => IsEditable);
            }
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public IEnumerable<ProjectViewModel> Projects => _projects;

        /// <summary>
        /// Gets a value indicating whether this instance has projects.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has projects; otherwise, <c>false</c>.
        /// </value>
        public bool HasProjects => _projects.Any();

        /// <summary>
        /// Edit the connection.
        /// </summary>
        public abstract void Edit();

        /// <summary>
        /// Remove the connection.
        /// </summary>
        public void Remove()
        {
            var viewModel = _removeConnectionFactory.Create(SettingsId);

            var message = new NavigationMessage2(viewModel);

            _eventAggregator.PublishOnUIThread(message);
        }

        private void BuildMonitorConnectionError(object sender, BuildMonitorConnectionErrorEventArgs e)
        {
            if (ShouldExitHandler(e))
            {
                return;
            }

            IsErrored = true;
            IsBusy = false;
        }

        private void BuildMonitorConnectionProgressChanged(object sender, BuildMonitorConnectionProgressEventArgs e)
        {
            if (ShouldExitHandler(e))
            {
                return;
            }

            var isDirty = false;

            foreach (var project in e.Projects)
            {
                var projectToUpdate = _projects.SingleOrDefault(p => p.Id == project.Id);

                if (projectToUpdate != null)
                {
                    projectToUpdate.TryUpdate(project.Name);
                }
                else
                {
                    var projectToAdd = _projectFactory.Create(SettingsId, project);

                    _projects.Add(projectToAdd);

                    isDirty = true;
                }
            }

            var projectsToKeep = e.Projects.Select(project => project.Id).ToArray();

            var removedProjects = _projects.RemoveAll(project => !projectsToKeep.Contains(project.Id)) > 0;

            isDirty = isDirty || removedProjects;

            if (isDirty)
            {
                NotifyOfPropertyChange(() => Projects);
                NotifyOfPropertyChange(() => HasProjects);
            }

            IsBusy = false;
        }

        private bool ShouldExitHandler(BuildMonitorConnectionEventArgs e)
        {
            return SettingsId != e.SettingsId;
        }
    }
}