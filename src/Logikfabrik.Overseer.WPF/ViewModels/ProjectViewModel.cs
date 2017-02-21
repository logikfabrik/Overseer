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
        private readonly IProjectDigestViewModelFactory _digestFactory;
        private readonly Guid _settingsId;
        private List<BuildViewModel> _builds;
        private string _name;
        private bool _isBusy;
        private bool _isErrored;
        private ProjectDigestViewModel _digest;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectViewModel" /> class.
        /// </summary>
        /// <param name="buildMonitor">The build monitor.</param>
        /// <param name="buildFactory">The build factory.</param>
        /// <param name="digestFactory">The digest factory.</param>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="projectName">The project name.</param>
        public ProjectViewModel(IBuildMonitor buildMonitor, IBuildViewModelFactory buildFactory, IProjectDigestViewModelFactory digestFactory, Guid settingsId, string projectId, string projectName)
        {
            Ensure.That(buildMonitor).IsNotNull();
            Ensure.That(buildFactory).IsNotNull();
            Ensure.That(digestFactory).IsNotNull();
            Ensure.That(settingsId).IsNotEmpty();
            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            _buildFactory = buildFactory;
            _digestFactory = digestFactory;
            _settingsId = settingsId;
            Id = projectId;
            _name = projectName;
            _isBusy = true;
            _isErrored = false;
            _builds = new List<BuildViewModel>();

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
                _builds = value.ToList();
                NotifyOfPropertyChange(() => Builds);
                NotifyOfPropertyChange(() => HasBuilds);
            }
        }

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
        /// Gets the digest.
        /// </summary>
        /// <value>
        /// The digest.
        /// </value>
        public ProjectDigestViewModel Digest
        {
            get
            {
                return _digest;
            }

            private set
            {
                _digest = value;
                NotifyOfPropertyChange(() => Digest);
            }
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

            var currentBuilds = new List<BuildViewModel>(Builds);

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
                    var buildToAdd = _buildFactory.Create(e.Project.Name, build);

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
                Digest = _digestFactory.CreateProjectDigestViewModel(e.Builds);
            }

            IsBusy = false;
        }

        private bool ShouldExitHandler(BuildMonitorProjectEventArgs e)
        {
            return _settingsId != e.SettingsId || Id != e.Project.Id;
        }
    }
}