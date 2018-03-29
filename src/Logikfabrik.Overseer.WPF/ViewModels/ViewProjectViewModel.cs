// <copyright file="ViewProjectViewModel.cs" company="Logikfabrik">
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
    /// The <see cref="ViewProjectViewModel" /> class. View model for CI projects.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ViewProjectViewModel : ViewModel, IViewProjectViewModel
    {
        private readonly IApp _application;
        private readonly IEventAggregator _eventAggregator;
        private readonly IViewBuildViewModelFactory _viewBuildViewModelFactory;
        private readonly INavigationMessageFactory<ViewProjectViewModel> _navigationMessageFactory;

#pragma warning disable S1450 // Private fields only used as local variables in methods should become local variables

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly CollectionViewSource _orderedBuilds;
#pragma warning restore S1450 // Private fields only used as local variables in methods should become local variables
        private readonly BindableCollection<IViewBuildViewModel> _builds;
        private string _name;
        private bool _isBusy;
        private bool _isErrored;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewProjectViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="application">The application.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildTracker">The build tracker.</param>
        /// <param name="viewBuildViewModelFactory">The view build view model factory.</param>
        /// <param name="editFavoriteViewModelFactory">The edit favorite view model factory.</param>
        /// <param name="navigationMessageFactory">The navigation message factory.</param>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="projectId">The project identifier.</param>
#pragma warning disable S107 // Methods should not have too many parameters

        // ReSharper disable once InheritdocConsiderUsage
        public ViewProjectViewModel(
            IPlatformProvider platformProvider,
            IApp application,
            IEventAggregator eventAggregator,
            IBuildTracker buildTracker,
            IViewBuildViewModelFactory viewBuildViewModelFactory,
            IEditFavoriteViewModelFactory editFavoriteViewModelFactory,
            INavigationMessageFactory<ViewProjectViewModel> navigationMessageFactory,
            Guid settingsId,
            string projectId)
            : this(platformProvider, application, eventAggregator, buildTracker, viewBuildViewModelFactory, editFavoriteViewModelFactory, navigationMessageFactory, settingsId, projectId, null)
        {
#pragma warning restore S107 // Methods should not have too many parameters
        }

        public ViewProjectViewModel(
            IPlatformProvider platformProvider,
            IApp application,
            IEventAggregator eventAggregator,
            IBuildTracker buildTracker,
            IViewBuildViewModelFactory viewBuildViewModelFactory,
            IEditFavoriteViewModelFactory editFavoriteViewModelFactory,
            INavigationMessageFactory<ViewProjectViewModel> navigationMessageFactory,
            Guid settingsId,
            string projectId,
            string projectName)
            : base(platformProvider)
        {
#pragma warning restore S107 // Methods should not have too many parameters
            Ensure.That(application).IsNotNull();
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildTracker).IsNotNull();
            Ensure.That(viewBuildViewModelFactory).IsNotNull();
            Ensure.That(editFavoriteViewModelFactory).IsNotNull();
            Ensure.That(navigationMessageFactory).IsNotNull();
            Ensure.That(settingsId).IsNotEmpty();
            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            _application = application;
            _eventAggregator = eventAggregator;
            _viewBuildViewModelFactory = viewBuildViewModelFactory;
            _navigationMessageFactory = navigationMessageFactory;
            SettingsId = settingsId;
            Id = projectId;
            _name = projectName;
            _isBusy = true;
            _isErrored = false;
            DisplayName = Properties.Resources.ViewProject_View;

            WeakEventManager<IBuildTracker, BuildTrackerProjectErrorEventArgs>.AddHandler(buildTracker, nameof(buildTracker.ProjectError), BuildTrackerProjectError);
            WeakEventManager<IBuildTracker, BuildTrackerProjectProgressEventArgs>.AddHandler(buildTracker, nameof(buildTracker.ProjectProgressChanged), BuildTrackerProjectProgressChanged);

            _builds = new BindableCollection<IViewBuildViewModel>();

            Favorite = editFavoriteViewModelFactory.Create(settingsId, projectId);

            _orderedBuilds = new CollectionViewSource
            {
                Source = _builds
            };

            OrderedBuilds = _orderedBuilds.View;
        }

        /// <inheritdoc />
        public Guid SettingsId { get; }

        /// <inheritdoc />
        public string Id { get; }

        /// <inheritdoc />
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        /// <inheritdoc />
        public ICollectionView OrderedBuilds { get; }

        /// <inheritdoc />
        public IViewBuildViewModel LatestBuild => _builds.FirstOrDefault(build => build.Status.IsInProgressOrFinished());

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

            set
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

            set
            {
                _isErrored = value;
                NotifyOfPropertyChange(() => IsErrored);
                NotifyOfPropertyChange(() => IsViewable);
            }
        }

        /// <inheritdoc />
        public EditFavoriteViewModel Favorite { get; }

        /// <inheritdoc />
        public void View()
        {
            var message = _navigationMessageFactory.Create(this);

            _eventAggregator.PublishOnUIThread(message);
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
                    buildToUpdate.Update(e.Project, build);
                }
                else
                {
                    _application.Dispatcher.Invoke(() =>
                    {
                        var buildToAdd = _viewBuildViewModelFactory.Create(e.Project, build);

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
            return SettingsId != e.SettingsId || Id != e.Project.Id;
        }
    }
}