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

    /// <summary>
    /// The <see cref="ProjectViewModel" /> class. View model for CI projects.
    /// </summary>
    public class ProjectViewModel : PropertyChangedBase
    {
        private readonly Guid _settingsId;
        private readonly string _projectId;
        private IEnumerable<BuildViewModel> _builds;
        private string _projectName;
        private bool _isBusy;
        private bool _isErrored;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectViewModel" /> class.
        /// </summary>
        /// <param name="buildMonitor">The build monitor.</param>
        /// <param name="settingsId">The settings ID.</param>
        /// <param name="projectId">The project ID.</param>
        public ProjectViewModel(IBuildMonitor buildMonitor, Guid settingsId, string projectId)
        {
            Ensure.That(buildMonitor).IsNotNull();
            Ensure.That(settingsId).IsNotEmpty();
            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            _settingsId = settingsId;
            _projectId = projectId;
            _isBusy = true;
            _isErrored = false;

            WeakEventManager<IBuildMonitor, BuildMonitorProjectErrorEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.ProjectError), BuildMonitorProjectError);
            WeakEventManager<IBuildMonitor, BuildMonitorProjectProgressEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.ProjectProgressChanged), BuildMonitorProjectProgressChanged);
        }

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
        public IEnumerable<BuildViewModel> Builds
        {
            get
            {
                return _builds;
            }

            private set
            {
                _builds = value;
                NotifyOfPropertyChange(() => Builds);
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

            Builds = e.Builds.Any()
                ? e.Builds.Select(build => new BuildViewModel(e.Project, build))
                : new BuildViewModel[] { };

            IsBusy = false;
        }

        private bool ShouldExitHandler(BuildMonitorProjectEventArgs e)
        {
            return _settingsId != e.SettingsId || _projectId != e.Project.Id;
        }
    }
}