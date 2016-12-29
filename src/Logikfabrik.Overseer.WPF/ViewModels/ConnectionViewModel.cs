﻿// <copyright file="ConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Data;
    using Caliburn.Micro;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="ConnectionViewModel" /> class.
    /// </summary>
    public abstract class ConnectionViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IBuildMonitor _buildMonitor;
        private readonly ConnectionSettings _settings;
        private bool _isBusy;
        private bool _isErrored;
        private object _lock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildMonitor">The build monitor.</param>
        /// <param name="settings">The settings.</param>
        protected ConnectionViewModel(IEventAggregator eventAggregator, IBuildMonitor buildMonitor, ConnectionSettings settings)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildMonitor).IsNotNull();
            Ensure.That(settings).IsNotNull();

            _eventAggregator = eventAggregator;
            _buildMonitor = buildMonitor;
            _settings = settings;
            _isBusy = true;
            _isErrored = false;
            Projects = new ObservableCollection<ProjectViewModel>();

            BindingOperations.EnableCollectionSynchronization(Projects, _lock);

            WeakEventManager<IBuildMonitor, BuildMonitorConnectionErrorEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.ConnectionError), BuildMonitorConnectionError);
            WeakEventManager<IBuildMonitor, BuildMonitorConnectionProgressEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.ConnectionProgressChanged), BuildMonitorConnectionProgressChanged);
        }

        /// <summary>
        /// Gets the connection name.
        /// </summary>
        /// <value>
        /// The connection name.
        /// </value>
        public string ConnectionName => _settings.Name;

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
                NotifyOfPropertyChange(() => IsNotBusy);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is not busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is not busy; otherwise, <c>false</c>.
        /// </value>
        public bool IsNotBusy
        {
            get
            {
                return !_isBusy;
            }

            private set
            {
                _isBusy = !value;
                NotifyOfPropertyChange(() => IsBusy);
                NotifyOfPropertyChange(() => IsNotBusy);
            }
        }

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
            }
        }

        /// <summary>
        /// Gets the projects
        /// </summary>
        /// <value>
        /// The projects
        /// </value>
        public ObservableCollection<ProjectViewModel> Projects { get; }

        /// <summary>
        /// Gets the type of the view model to edit the connection.
        /// </summary>
        /// <value>
        /// The type of the view model to edit the connection.
        /// </value>
        protected abstract Type EditConnectionViewModelType { get; }

        /// <summary>
        /// Edit the connection.
        /// </summary>
        public void Edit()
        {
            // TODO: Navigate to the view/view model to edit, including the current settings for the provider to edit.
            var message = new NavigationMessage(EditConnectionViewModelType);

            _eventAggregator.PublishOnUIThread(message);
        }

        /// <summary>
        /// Remove the connection.
        /// </summary>
        public void Remove()
        {
            // TODO: Navigate to the right view/view model.
            throw new NotImplementedException();
        }

        private void BuildMonitorConnectionError(object sender, BuildMonitorConnectionErrorEventArgs e)
        {
            if (ShouldExitHandler(e))
            {
                return;
            }

            IsErrored = true;
        }

        private void BuildMonitorConnectionProgressChanged(object sender, BuildMonitorConnectionProgressEventArgs e)
        {
            if (ShouldExitHandler(e))
            {
                return;
            }

            Projects.Clear();

            // TODO: Rewrite.
            foreach (var project in e.Projects)
            {
                Projects.Add(new ProjectViewModel(_buildMonitor, _settings.Id, project.Id) { ProjectName = project.Name });
            }

            IsBusy = false;
        }

        private bool ShouldExitHandler(BuildMonitorConnectionEventArgs e)
        {
            return _settings.Id != e.SettingsId;
        }
    }
}