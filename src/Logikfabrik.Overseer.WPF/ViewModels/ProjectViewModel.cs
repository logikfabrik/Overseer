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
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="ProjectViewModel" /> class. View model for CI projects.
    /// </summary>
    public class ProjectViewModel : ViewModel, IProjectViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IBuildViewModelFactory _buildFactory;
        private readonly Guid _settingsId;
        private List<IBuildViewModel> _builds;
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
            _builds = new List<IBuildViewModel>();
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
        public IEnumerable<IBuildViewModel> Builds
        {
            get
            {
                return _builds;
            }

            private set
            {
                _builds = value.ToList();
                NotifyOfPropertyChange(() => Builds);
                NotifyOfPropertyChange(() => HasBuilds);
                NotifyOfPropertyChange(() => LatestBuild);
            }
        }

        /// <summary>
        /// Gets the latest build.
        /// </summary>
        /// <value>
        /// The latest build.
        /// </value>
        public IBuildViewModel LatestBuild => _builds.FirstOrDefault();

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
        /// View the connection.
        /// </summary>
        public void View()
        {
            var message = new NavigationMessage2(this);

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

            var isDirty = false;
            var isUpdated = false;

            var currentBuilds = new List<IBuildViewModel>(Builds);

            foreach (var build in e.Builds)
            {
                var buildToUpdate = currentBuilds.SingleOrDefault(b => b.Id == build.Id);

                if (buildToUpdate != null)
                {
                    buildToUpdate.TryUpdate(e.Project.Name, build.Status, build.StartTime, build.EndTime, build.GetRunTime());
                    isUpdated = true;
                }
                else
                {
                    var buildToAdd = _buildFactory.Create(e.Project.Name, build.Id, build.Branch, build.GetVersionNumber(), build.RequestedBy, build.Changes, build.Status, build.StartTime, build.EndTime, build.GetRunTime());

                    currentBuilds.Add(buildToAdd);
                    isDirty = true;
                }
            }

            var buildsToKeep = e.Builds.Select(build => build.Id).ToArray();

            var removedBuilds = currentBuilds.RemoveAll(build => !buildsToKeep.Contains(build.Id)) > 0;

            isDirty = isDirty || removedBuilds;

            if (isDirty || isUpdated)
            {
                Builds = currentBuilds.OrderByDescending(build => build.StartTime ?? DateTime.MaxValue);
            }

            IsBusy = false;
        }

        private bool ShouldExitHandler(BuildMonitorProjectEventArgs e)
        {
            return _settingsId != e.SettingsId || Id != e.Project.Id;
        }
    }
}