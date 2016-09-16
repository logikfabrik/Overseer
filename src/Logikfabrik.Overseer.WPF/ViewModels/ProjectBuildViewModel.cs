// <copyright file="ProjectBuildViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
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
        private BuildViewModel _buildViewModel;

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

            WeakEventManager<IBuildMonitor, BuildMonitorProgressEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.ProgressChanged), BuildMonitorProgressChanged);
        }

        /// <summary>
        /// Gets the project name.
        /// </summary>
        /// <value>
        /// The project name.
        /// </value>
        public string ProjectName => _project.Name;

        /// <summary>
        /// Gets the build view model.
        /// </summary>
        /// <value>
        /// The build view model.
        /// </value>
        public BuildViewModel BuildViewModel
        {
            get
            {
                return _buildViewModel;
            }

            private set
            {
                _buildViewModel = value;
                NotifyOfPropertyChange(() => BuildViewModel);
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

            BuildViewModel = new BuildViewModel(e.Builds.FirstOrDefault());
        }
    }
}
