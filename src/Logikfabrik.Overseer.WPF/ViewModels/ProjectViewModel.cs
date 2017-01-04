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
    public class ProjectViewModel : PropertyChangedBase
    {
        private readonly IBuildViewModelFactory _buildFactory;
        private readonly Guid _settingsId;
        private List<BuildViewModel> _builds;
        private string _projectName;
        private bool _isBusy;
        private bool _isErrored;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectViewModel" /> class.
        /// </summary>
        /// <param name="buildMonitor">The build monitor.</param>
        /// <param name="buildFactory">The build factory.</param>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        public ProjectViewModel(IBuildMonitor buildMonitor, IBuildViewModelFactory buildFactory, Guid settingsId, string projectId)
        {
            Ensure.That(buildMonitor).IsNotNull();
            Ensure.That(buildFactory).IsNotNull();
            Ensure.That(settingsId).IsNotEmpty();
            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            _buildFactory = buildFactory;
            _settingsId = settingsId;
            ProjectId = projectId;
            _isBusy = true;
            _isErrored = false;
            _builds = new List<BuildViewModel>();

            WeakEventManager<IBuildMonitor, BuildMonitorProjectErrorEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.ProjectError), BuildMonitorProjectError);
            WeakEventManager<IBuildMonitor, BuildMonitorProjectProgressEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.ProjectProgressChanged), BuildMonitorProjectProgressChanged);
        }

        /// <summary>
        /// Gets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        public string ProjectId { get; }

        /// <summary>
        /// Gets or sets the project name.
        /// </summary>
        /// <value>
        /// The project name.
        /// </value>
        public string ProjectName
        {
            get
            {
                return _projectName;
            }

            set
            {
                _projectName = value;
                NotifyOfPropertyChange(() => ProjectName);
            }
        }

        /// <summary>
        /// Gets the build view models.
        /// </summary>
        /// <value>
        /// The build view models.
        /// </value>
        public IEnumerable<BuildViewModel> Builds => _builds;

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
        public bool IsNotBusy => !_isBusy;

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

        private void BuildMonitorProjectError(object sender, BuildMonitorProjectErrorEventArgs e)
        {
            if (ShouldExitHandler(e))
            {
                return;
            }

            IsErrored = true;
        }

        private void BuildMonitorProjectProgressChanged(object sender, BuildMonitorProjectProgressEventArgs e)
        {
            if (ShouldExitHandler(e))
            {
                return;
            }

            var isDirty = false;

            foreach (var build in e.Builds)
            {
                var buildToUpdate = _builds.SingleOrDefault(b => b.BuildId == build.Id);

                if (buildToUpdate != null)
                {
                    buildToUpdate.SetProjectName(e.Project.Name);
                    buildToUpdate.SetVersionNumber(build.GetVersionNumber());
                    buildToUpdate.SetBranch(build.Branch);
                    buildToUpdate.Status = build.Status;
                    buildToUpdate.Started = build.Started;
                    buildToUpdate.SetBuildTime(build.GetBuildTime());
                }
                else
                {
                    var buildToAdd = _buildFactory.Create(e.Project.Name, build);

                    _builds.Add(buildToAdd);

                    isDirty = true;
                }
            }

            var buildsToKeep = e.Builds.Select(build => build.Id).ToArray();

            var removedBuilds = _builds.RemoveAll(build => !buildsToKeep.Contains(build.BuildId)) > 0;

            isDirty = isDirty || removedBuilds;

            if (isDirty)
            {
                _builds = _builds.OrderByDescending(build => build.Started ?? DateTime.MaxValue).ToList();
                NotifyOfPropertyChange(() => Builds);
            }

            IsBusy = false;
        }

        private bool ShouldExitHandler(BuildMonitorProjectEventArgs e)
        {
            return _settingsId != e.SettingsId || ProjectId != e.Project.Id;
        }
    }
}