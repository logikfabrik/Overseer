// <copyright file="ConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionViewModel" /> class.
    /// </summary>
    public class ConnectionViewModel : PropertyChangedBase
    {
        private readonly Lazy<IEnumerable<ProjectBuildViewModel>> _projectBuildViewModels;
        private bool _isBusy;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionViewModel" /> class.
        /// </summary>
        /// <param name="buildMonitor">The build monitor.</param>
        /// <param name="buildProvider">The build provider settings.</param>
        public ConnectionViewModel(IBuildMonitor buildMonitor, IBuildProvider buildProvider)
        {
            Ensure.That(buildMonitor).IsNotNull();
            Ensure.That(buildProvider).IsNotNull();

            Name = buildProvider.BuildProviderSettings.Name;
            BuildProviderName = buildProvider.Name;
            _isBusy = true;

            _projectBuildViewModels = new Lazy<IEnumerable<ProjectBuildViewModel>>(() =>
            {
                var projectViewModels = buildProvider.GetProjects().Select(project => new ProjectBuildViewModel(buildMonitor, buildProvider, project));

                IsBusy = false;

                return projectViewModels;
            });
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the build provider name.
        /// </summary>
        /// <value>
        /// The build provider name.
        /// </value>
        public string BuildProviderName { get; }

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
        /// Gets the project build view models.
        /// </summary>
        /// <value>
        /// The project build view models.
        /// </value>
        public IEnumerable<ProjectBuildViewModel> ProjectBuildViewModels => _projectBuildViewModels.Value;
    }
}
