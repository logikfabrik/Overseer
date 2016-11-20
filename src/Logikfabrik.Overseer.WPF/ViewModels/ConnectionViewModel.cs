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

    /// <summary>
    /// The <see cref="ConnectionViewModel" /> class.
    /// </summary>
    public abstract class ConnectionViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IBuildProvider _buildProvider;
        private readonly Lazy<IEnumerable<ProjectBuildViewModel>> _projectBuildViewModels;
        private bool _isBusy;
        private bool _isErrored;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildMonitor">The build monitor.</param>
        /// <param name="buildProvider">The build provider.</param>
        protected ConnectionViewModel(IEventAggregator eventAggregator, IBuildMonitor buildMonitor, IBuildProvider buildProvider)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildMonitor).IsNotNull();
            Ensure.That(buildProvider).IsNotNull();

            _eventAggregator = eventAggregator;
            _buildProvider = buildProvider;
            _isBusy = true;
            _isErrored = false;
            _projectBuildViewModels = new Lazy<IEnumerable<ProjectBuildViewModel>>(() =>
            {
                var projectViewModels = buildProvider.GetProjectsAsync().Result.Select(project => new ProjectBuildViewModel(buildMonitor, buildProvider, project));

                IsBusy = false;

                return projectViewModels;
            });

            WeakEventManager<IBuildMonitor, BuildMonitorErrorEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.Error), BuildMonitorError);

            buildMonitor.StartMonitoring();
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => _buildProvider.BuildProviderSettings.Name;

        /// <summary>
        /// Gets the build provider name.
        /// </summary>
        /// <value>
        /// The build provider name.
        /// </value>
        public abstract string BuildProviderName { get; }

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
        /// Gets the project build view models.
        /// </summary>
        /// <value>
        /// The project build view models.
        /// </value>
        public IEnumerable<ProjectBuildViewModel> ProjectBuildViewModels => _projectBuildViewModels.Value;

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

        private void BuildMonitorError(object sender, BuildMonitorErrorEventArgs e)
        {
            if (e.BuildProvider == null)
            {
                return;
            }

            if (e.Project != null)
            {
                return;
            }

            if (_buildProvider.BuildProviderSettings.Id != e.BuildProvider.BuildProviderSettings.Id)
            {
                return;
            }

            IsErrored = true;
        }
    }
}