﻿// <copyright file="ProjectViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
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
        private readonly IApp _application;
        private readonly IEventAggregator _eventAggregator;
        private readonly IBuildViewModelFactory _buildFactory;
        private readonly Guid _settingsId;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly CollectionViewSource _orderedBuilds;
        private readonly BindableCollection<IBuildViewModel> _builds;
        private string _name;
        private bool _isBusy;
        private bool _isErrored;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectViewModel" /> class.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildTracker">The build tracker.</param>
        /// <param name="buildFactory">The build factory.</param>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="projectName">The project name.</param>
        public ProjectViewModel(
            IApp application,
            IPlatformProvider platformProvider,
            IEventAggregator eventAggregator,
            IBuildTracker buildTracker,
            IBuildViewModelFactory buildFactory,
            Guid settingsId,
            string projectId,
            string projectName)
            : base(platformProvider)
        {
            Ensure.That(application).IsNotNull();
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildTracker).IsNotNull();
            Ensure.That(buildFactory).IsNotNull();
            Ensure.That(settingsId).IsNotEmpty();
            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            _application = application;
            _eventAggregator = eventAggregator;
            _buildFactory = buildFactory;
            _settingsId = settingsId;
            Id = projectId;
            _name = projectName;
            _isBusy = true;
            _isErrored = false;
            DisplayName = Properties.Resources.Project_View;

            WeakEventManager<IBuildTracker, BuildTrackerProjectErrorEventArgs>.AddHandler(buildTracker, nameof(buildTracker.ProjectError), BuildTrackerProjectError);
            WeakEventManager<IBuildTracker, BuildTrackerProjectProgressEventArgs>.AddHandler(buildTracker, nameof(buildTracker.ProjectProgressChanged), BuildTrackerProjectProgressChanged);

            _builds = new BindableCollection<IBuildViewModel>();

            _orderedBuilds = new CollectionViewSource
            {
                Source = _builds
            };

            OrderedBuilds = _orderedBuilds.View;
        }

        /// <inheritdoc />
        public string Id { get; }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public ICollectionView OrderedBuilds { get; }

        /// <inheritdoc />
        public IBuildViewModel LatestBuild => _builds.FirstOrDefault(build => build.Status.IsInProgressOrFinished());

        /// <inheritdoc />
        public int QueuedBuilds => _builds.Count(build => build.Status == BuildStatus.Queued);

        /// <inheritdoc />
        public bool HasBuilds => !IsBusy && _builds.Any();

        /// <inheritdoc />
        public bool HasNoBuilds => !IsBusy && !_builds.Any();

        /// <inheritdoc />
        public bool HasLatestBuild => LatestBuild != null;

        /// <inheritdoc />
        public bool HasQueuedBuilds => QueuedBuilds > 0;

        /// <inheritdoc />
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

        /// <inheritdoc />
        public bool IsViewable => !IsErrored && !IsBusy && HasBuilds;

        /// <inheritdoc />
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

        /// <inheritdoc/>
        public void View()
        {
            var message = new NavigationMessage(this);

            _eventAggregator.PublishOnUIThread(message);
        }

        /// <inheritdoc/>
        public bool TryUpdate(string name)
        {
            if (Name == name)
            {
                return false;
            }

            Name = name;

            return true;
        }

        private void BuildTrackerProjectError(object sender, BuildTrackerProjectErrorEventArgs e)
        {
            if (ShouldExitHandler(e))
            {
                return;
            }

            IsErrored = true;
            IsBusy = false;
        }

        private void BuildTrackerProjectProgressChanged(object sender, BuildTrackerProjectProgressEventArgs e)
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
                    _application.Dispatcher.Invoke(() =>
                    {
                        var buildToAdd = _buildFactory.Create(e.Project.Name, build.Id, build.Branch, build.VersionNumber(), build.RequestedBy, build.Changes, build.Status, build.StartTime, build.EndTime, build.RunTime());

                        var time = new Tuple<DateTime, DateTime>(buildToAdd.EndTime ?? DateTime.MaxValue, buildToAdd.StartTime ?? DateTime.MaxValue);

                        var times = _builds.Select(b => new Tuple<DateTime, DateTime>(b.EndTime ?? DateTime.MaxValue, b.StartTime ?? DateTime.MaxValue)).Concat(new[] { time }).OrderByDescending(t => t.Item1).ThenByDescending(t => t.Item2).ToArray();

                        var index = Array.IndexOf(times, time);

                        _builds.Insert(index, buildToAdd);
                    });
                }
            }

            var buildsToKeep = e.Builds.Select(build => build.Id).ToArray();
            var buildsToRemove = _builds.Where(build => !buildsToKeep.Contains(build.Id)).ToArray();

            if (buildsToRemove.Any())
            {
                _application.Dispatcher.Invoke(() =>
                {
                    _builds.RemoveRange(buildsToRemove);
                });
            }

            IsErrored = false;
            IsBusy = false;

            NotifyOfPropertyChange(() => HasBuilds);
            NotifyOfPropertyChange(() => HasNoBuilds);
            NotifyOfPropertyChange(() => LatestBuild);
            NotifyOfPropertyChange(() => HasLatestBuild);
            NotifyOfPropertyChange(() => QueuedBuilds);
            NotifyOfPropertyChange(() => HasQueuedBuilds);
            NotifyOfPropertyChange(() => IsViewable);
        }

        private bool ShouldExitHandler(BuildTrackerProjectEventArgs e)
        {
            return _settingsId != e.SettingsId || Id != e.Project.Id;
        }
    }
}