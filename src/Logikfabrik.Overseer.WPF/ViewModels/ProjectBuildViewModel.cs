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

    /// <summary>
    /// The <see cref="ProjectBuildViewModel" /> class.
    /// </summary>
    public class ProjectBuildViewModel : PropertyChangedBase
    {
        private readonly IBuildProvider _buildProvider;
        private readonly IProject _project;
        private IEnumerable<BuildViewModel> _buildViewModels;
        private bool _isBusy;
        private bool _isErrored;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectBuildViewModel" /> class.
        /// </summary>
        /// <param name="buildMonitor">The build monitor.</param>
        /// <param name="buildProvider">The build provider.</param>
        /// <param name="project">The project.</param>
        public ProjectBuildViewModel(IBuildMonitor buildMonitor, IBuildProvider buildProvider, IProject project)
        {
            Ensure.That(buildMonitor).IsNotNull();
            Ensure.That(buildProvider).IsNotNull();
            Ensure.That(project).IsNotNull();

            _buildProvider = buildProvider;
            _project = project;
            _isBusy = true;
            _isErrored = false;

            WeakEventManager<IBuildMonitor, BuildMonitorProgressEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.ProgressChanged), BuildMonitorProgressChanged);
            WeakEventManager<IBuildMonitor, BuildMonitorErrorEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.Error), BuildMonitorError);
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
        public IEnumerable<BuildViewModel> BuildViewModels
        {
            get
            {
                return _buildViewModels;
            }

            private set
            {
                _buildViewModels = value;
                NotifyOfPropertyChange(() => BuildViewModels);
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

        private void BuildMonitorProgressChanged(object sender, BuildMonitorProgressEventArgs e)
        {
            if (_buildProvider.BuildProviderSettings.Id != e.BuildProvider.BuildProviderSettings.Id)
            {
                return;
            }

            if (_project.Id != e.Project.Id)
            {
                return;
            }

            if (e.Builds.Any())
            {
                BuildViewModels = e.Builds.Select(build => new BuildViewModel(e.Project, build));
            }

            IsBusy = false;
        }

        private void BuildMonitorError(object sender, BuildMonitorErrorEventArgs e)
        {
            if (e.BuildProvider == null)
            {
                return;
            }

            if (e.Project == null)
            {
                return;
            }

            if (_buildProvider.BuildProviderSettings.Id != e.BuildProvider.BuildProviderSettings.Id)
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
