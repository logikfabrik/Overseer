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
        private readonly IBuildProvider _provider;
        private readonly Lazy<IEnumerable<ProjectBuildViewModel>> _projects;
        private bool _isBusy;
        private bool _isErrored;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildMonitor">The build monitor.</param>
        /// <param name="provider">The provider.</param>
        protected ConnectionViewModel(IEventAggregator eventAggregator, IBuildMonitor buildMonitor, IBuildProvider provider)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildMonitor).IsNotNull();
            Ensure.That(provider).IsNotNull();

            _eventAggregator = eventAggregator;
            _provider = provider;
            _isBusy = true;
            _isErrored = false;
            _projects = new Lazy<IEnumerable<ProjectBuildViewModel>>(() =>
            {
                var projects = provider.GetProjectsAsync().Result.Select(project => new ProjectBuildViewModel(buildMonitor, provider, project));

                IsBusy = false;

                return projects;
            });

            WeakEventManager<IBuildMonitor, BuildMonitorErrorEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.Error), BuildMonitorError);

            buildMonitor.StartMonitoring();
        }

        /// <summary>
        /// Gets the connection name.
        /// </summary>
        /// <value>
        /// The connection name.
        /// </value>
        public string ConnectionName => _provider.Settings.Name;

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
        public IEnumerable<ProjectBuildViewModel> Projects => _projects.Value;

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
            if (e.Provider == null)
            {
                return;
            }

            if (e.Project != null)
            {
                return;
            }

            if (_provider.Settings.Id != e.Provider.Settings.Id)
            {
                return;
            }

            IsErrored = true;
        }
    }
}