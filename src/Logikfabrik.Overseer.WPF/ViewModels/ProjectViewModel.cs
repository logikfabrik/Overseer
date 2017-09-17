// <copyright file="ProjectViewModel.cs" company="Logikfabrik">
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
    using Navigation;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="ProjectViewModel" /> class. View model for CI projects.
    /// </summary>
    public class ProjectViewModel : ViewModel, IProjectViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IBuildViewModelFactory _buildFactory;
        private readonly Guid _settingsId;
        private readonly BindableCollection<IBuildViewModel> _builds;
        private string _name;
        private bool _isBusy;
        private bool _isErrored;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildMonitor">The build monitor.</param>
        /// <param name="buildFactory">The build factory.</param>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="projectName">The project name.</param>
        public ProjectViewModel(
            IEventAggregator eventAggregator,
            IBuildMonitor buildMonitor,
            IBuildViewModelFactory buildFactory,
            Guid settingsId,
            string projectId,
            string projectName)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildMonitor).IsNotNull();
            Ensure.That(buildFactory).IsNotNull();
            Ensure.That(settingsId).IsNotEmpty();
            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            _eventAggregator = eventAggregator;
            _buildFactory = buildFactory;
            _settingsId = settingsId;
            Id = projectId;
            _name = projectName;
            _isBusy = true;
            _isErrored = false;
            _builds = new BindableCollection<IBuildViewModel>();
            DisplayName = "Project";

            WeakEventManager<IBuildMonitor, BuildMonitorProjectErrorEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.ProjectError), BuildMonitorProjectError);
            WeakEventManager<IBuildMonitor, BuildMonitorProjectProgressEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.ProjectProgressChanged), BuildMonitorProjectProgressChanged);
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; }

        /// <summary>
        /// Gets the name.
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

            private set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        /// <summary>
        /// Gets the builds.
        /// </summary>
        /// <value>
        /// The builds.
        /// </value>
        public IEnumerable<IBuildViewModel> Builds => _builds;

        /// <summary>
        /// Gets the latest build.
        /// </summary>
        /// <value>
        /// The latest build.
        /// </value>
        public IBuildViewModel LatestBuild => Builds.FirstOrDefault();

        /// <summary>
        /// Gets a value indicating whether this instance has builds.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has builds; otherwise, <c>false</c>.
        /// </value>
        public bool HasBuilds => Builds.Any();

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
                NotifyOfPropertyChange(() => IsViewable);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is viewable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is viewable; otherwise, <c>false</c>.
        /// </value>
        public bool IsViewable => !IsErrored && !IsBusy && HasBuilds;

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
                NotifyOfPropertyChange(() => IsViewable);
            }
        }

        /// <summary>
        /// View the connection.
        /// </summary>
        public void View()
        {
            var message = new NavigationMessage(this);

            _eventAggregator.PublishOnUIThread(message);
        }

        /// <summary>
        /// Tries to update this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if this instance was updated; otherwise, <c>false</c>.</returns>
        public bool TryUpdate(string name)
        {
            if (Name == name)
            {
                return false;
            }

            Name = name;

            return true;
        }

        private void BuildMonitorProjectError(object sender, BuildMonitorProjectErrorEventArgs e)
        {
            if (ShouldExitHandler(e))
            {
                return;
            }

            IsErrored = true;
            IsBusy = false;
        }

        private void BuildMonitorProjectProgressChanged(object sender, BuildMonitorProjectProgressEventArgs e)
        {
            if (ShouldExitHandler(e))
            {
                return;
            }

            foreach (var build in e.Builds)
            {
                var buildToUpdate = _builds.SingleOrDefault(b => b.Id == build.Id);

                if (buildToUpdate != null)
                {
                    buildToUpdate.TryUpdate(e.Project.Name, build.Status, build.StartTime, build.EndTime, build.RunTime());
                }
                else
                {
                    var buildToAdd = _buildFactory.Create(e.Project.Name, build.Id, build.Branch, build.VersionNumber(), build.RequestedBy, build.Changes, build.Status, build.StartTime, build.EndTime, build.RunTime());

                    _builds.Insert(0, buildToAdd);
                }
            }

            var buildsToKeep = e.Builds.Select(build => build.Id).ToArray();
            var buildsToRemove = _builds.Where(build => !buildsToKeep.Contains(build.Id)).ToArray();

            _builds.RemoveRange(buildsToRemove);

            IsErrored = false;
            IsBusy = false;

            NotifyOfPropertyChange(() => HasBuilds);
            NotifyOfPropertyChange(() => LatestBuild);
            NotifyOfPropertyChange(() => IsViewable);
        }

        private bool ShouldExitHandler(BuildMonitorProjectEventArgs e)
        {
            return _settingsId != e.SettingsId || Id != e.Project.Id;
        }
    }
}