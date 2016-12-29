// <copyright file="ProjectBuildViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using Caliburn.Micro;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="ProjectBuildViewModel" /> class.
    /// </summary>
    public class ProjectBuildViewModel : PropertyChangedBase
    {
        private readonly ConnectionSettings _settings;
        private readonly IProject _project;
        private IEnumerable<BuildViewModel> _builds;
        private bool _isBusy;
        private bool _isErrored;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectBuildViewModel" /> class.
        /// </summary>
        /// <param name="buildMonitor">The build monitor.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="project">The project.</param>
        public ProjectBuildViewModel(IBuildMonitor buildMonitor, ConnectionSettings settings, IProject project)
        {
            Ensure.That(buildMonitor).IsNotNull();
            Ensure.That(settings).IsNotNull();
            Ensure.That(project).IsNotNull();

            _settings = settings;
            _project = project;
            _isBusy = true;
            _isErrored = false;

            WeakEventManager<IBuildMonitor, BuildMonitorProjectErrorEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.ProjectError), BuildMonitorProjectError);
            WeakEventManager<IBuildMonitor, BuildMonitorProjectProgressEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.ProjectProgressChanged), BuildMonitorProjectProgressChanged);
        }

        /// <summary>
        /// Gets the project name.
        /// </summary>
        /// <value>
        /// The project name.
        /// </value>
        public string ProjectName => _project.Name;

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

        private void BuildMonitorProjectProgressChanged(object sender, BuildMonitorProjectProgressEventArgs e)
        {
            if (_settings.Id != e.Settings.Id)
            {
                return;
            }

            if (_project.Id != e.Project.Id)
            {
                return;
            }

            Builds = e.Builds.Any()
                ? e.Builds.Select(build => new BuildViewModel(e.Project, build))
                : new BuildViewModel[] { };

            IsBusy = false;
        }

        private void BuildMonitorProjectError(object sender, BuildMonitorProjectErrorEventArgs e)
        {
            if (_settings.Id != e.Settings.Id)
            {
                return;
            }

            if (_project.Id != e.Project.Id)
            {
                return;
            }

            IsErrored = true;
        }
    }
}
