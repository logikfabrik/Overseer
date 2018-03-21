// <copyright file="ViewConnectionViewModel{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    using Caliburn.Micro;
    using EnsureThat;
    using Factories;
    using Gma.DataStructures.StringSearch;
    using Navigation;
    using Settings;

    /// <summary>
    /// The <see cref="ViewConnectionViewModel{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The <see cref="ConnectionSettings" /> type.</typeparam>
    // ReSharper disable once InheritdocConsiderUsage
    public class ViewConnectionViewModel<T> : ViewModel, IViewConnectionViewModel
        where T : ConnectionSettings
    {
        private readonly IApp _application;
        private readonly IEventAggregator _eventAggregator;
        private readonly IViewProjectViewModelFactory _viewProjectViewModelFactory;
        private readonly IRemoveConnectionViewModelFactory _removeConnectionViewModelFactory;
        private readonly IEditConnectionViewModelFactory<T> _editConnectionViewModelFactory;
        private readonly INavigationMessageFactory<EditConnectionViewModel<T>> _editConnectionViewModelNavigationMessageFactory;
        private readonly INavigationMessageFactory<RemoveConnectionViewModel> _removeConnectionViewModelNavigationMessageFactory;
        private readonly INavigationMessageFactory<ViewConnectionViewModel<T>> _viewConnectionViewModelNavigationMessageFactory;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly CollectionViewSource _filteredProjects;
        private readonly BindableCollection<IViewProjectViewModel> _projects;
        private SuffixTrie<IViewProjectViewModel> _trie;
        private string _filter;
        private IEnumerable<IViewProjectViewModel> _matches;
        private bool _isBusy;
        private bool _isErrored;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewConnectionViewModel{T}" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="application">The application.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildTracker">The build tracker.</param>
        /// <param name="viewProjectViewModelFactory">The view project view model factory.</param>
        /// <param name="removeConnectionViewModelFactory">The remove connection view model factory.</param>
        /// <param name="editConnectionViewModelFactory">The edit connection view model factory.</param>
        /// <param name="viewConnectionViewModelNavigationMessageFactory"></param>
        /// <param name="editConnectionViewModelNavigationMessageFactory"></param>
        /// <param name="removeConnectionViewModelNavigationMessageFactory"></param>
        /// <param name="settings">The settings.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public ViewConnectionViewModel(
            IPlatformProvider platformProvider,
            IApp application,
            IEventAggregator eventAggregator,
            IBuildTracker buildTracker,
            IViewProjectViewModelFactory viewProjectViewModelFactory,
            IRemoveConnectionViewModelFactory removeConnectionViewModelFactory,
            IEditConnectionViewModelFactory<T> editConnectionViewModelFactory,
            INavigationMessageFactory<EditConnectionViewModel<T>> editConnectionViewModelNavigationMessageFactory,
            INavigationMessageFactory<RemoveConnectionViewModel> removeConnectionViewModelNavigationMessageFactory,
            INavigationMessageFactory<ViewConnectionViewModel<T>> viewConnectionViewModelNavigationMessageFactory,
            T settings)
            : base(platformProvider)
        {
            Ensure.That(application).IsNotNull();
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildTracker).IsNotNull();
            Ensure.That(viewProjectViewModelFactory).IsNotNull();
            Ensure.That(removeConnectionViewModelFactory).IsNotNull();
            Ensure.That(editConnectionViewModelFactory).IsNotNull();
            Ensure.That(editConnectionViewModelNavigationMessageFactory).IsNotNull();
            Ensure.That(removeConnectionViewModelNavigationMessageFactory).IsNotNull();
            Ensure.That(viewConnectionViewModelNavigationMessageFactory).IsNotNull();
            Ensure.That(settings).IsNotNull();

            _application = application;
            _eventAggregator = eventAggregator;
            _viewProjectViewModelFactory = viewProjectViewModelFactory;
            _removeConnectionViewModelFactory = removeConnectionViewModelFactory;
            _editConnectionViewModelFactory = editConnectionViewModelFactory;
            _editConnectionViewModelNavigationMessageFactory = editConnectionViewModelNavigationMessageFactory;
            _removeConnectionViewModelNavigationMessageFactory = removeConnectionViewModelNavigationMessageFactory;
            _viewConnectionViewModelNavigationMessageFactory = viewConnectionViewModelNavigationMessageFactory;
            Settings = settings;
            _isBusy = true;
            _isErrored = false;
            DisplayName = Properties.Resources.ViewConnection_View;
            KeepAlive = true;

            WeakEventManager<IBuildTracker, BuildTrackerConnectionErrorEventArgs>.AddHandler(buildTracker, nameof(buildTracker.ConnectionError), BuildTrackerConnectionError);
            WeakEventManager<IBuildTracker, BuildTrackerConnectionProgressEventArgs>.AddHandler(buildTracker, nameof(buildTracker.ConnectionProgressChanged), BuildTrackerConnectionProgressChanged);

            _projects = new BindableCollection<IViewProjectViewModel>();

            _filteredProjects = new CollectionViewSource
            {
                Source = _projects
            };

            _filteredProjects.Filter += (sender, e) =>
            {
                var project = (IViewProjectViewModel)e.Item;

                e.Accepted = _matches?.Contains(project) ?? true;
            };

            FilteredProjects = _filteredProjects.View;
        }

        /// <inheritdoc />
        public Guid SettingsId => Settings.Id;

        /// <inheritdoc />
        public string SettingsName
        {
            get
            {
                return Settings.Name;
            }

            set
            {
                Settings.Name = value;
                NotifyOfPropertyChange(() => SettingsName);
            }
        }

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
                NotifyOfPropertyChange(() => IsEditable);
            }
        }

        /// <inheritdoc />
        public bool IsViewable => !IsErrored && !IsBusy && HasProjects;

        /// <inheritdoc />
        public bool IsEditable => IsErrored || !IsBusy;

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
                NotifyOfPropertyChange(() => IsEditable);
            }
        }

        /// <inheritdoc />
        public ICollectionView FilteredProjects { get; }

        /// <inheritdoc />
        public string Filter
        {
            get
            {
                return _filter;
            }

            set
            {
                _filter = value;
                NotifyOfPropertyChange(() => Filter);

                _matches = _trie.Retrieve(value?.ToLowerInvariant());

                FilteredProjects.Refresh();
            }
        }

        /// <inheritdoc />
        public bool HasProjects => !IsBusy && _projects.Any();

        /// <inheritdoc />
        public bool HasNoProjects => !IsBusy && !_projects.Any();

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        protected T Settings { get; }

        /// <inheritdoc />
        public void Edit()
        {
            var item = _editConnectionViewModelFactory.Create(Settings);

            var message = _editConnectionViewModelNavigationMessageFactory.Create(item);

            _eventAggregator.PublishOnUIThread(message);
        }

        /// <inheritdoc />
        public void Remove()
        {
            var item = _removeConnectionViewModelFactory.Create(this);

            var message = _removeConnectionViewModelNavigationMessageFactory.Create(item);

            _eventAggregator.PublishOnUIThread(message);
        }

        /// <inheritdoc />
        public void View()
        {
            var message = _viewConnectionViewModelNavigationMessageFactory.Create(this);

            _eventAggregator.PublishOnUIThread(message);
        }

        /// <inheritdoc />
        public void ClearFilter()
        {
            Filter = string.Empty;
        }

        private void BuildTrackerConnectionError(object sender, BuildTrackerConnectionErrorEventArgs e)
        {
            if (ShouldExitHandler(e))
            {
                return;
            }

            IsErrored = true;
            IsBusy = false;
        }

        private void BuildTrackerConnectionProgressChanged(object sender, BuildTrackerConnectionProgressEventArgs e)
        {
            if (ShouldExitHandler(e))
            {
                return;
            }

            foreach (var project in e.Projects)
            {
                var projectToUpdate = _projects.SingleOrDefault(p => p.Id == project.Id);

                if (projectToUpdate != null)
                {
                    projectToUpdate.TryUpdate(project.Name);
                }
                else
                {
                    _application.Dispatcher.Invoke(() =>
                    {
                        var projectToAdd = _viewProjectViewModelFactory.Create(SettingsId, project.Id, project.Name);

                        var names = _projects.Select(p => p.Name).Concat(new[] { projectToAdd.Name }).OrderBy(name => name).ToArray();

                        var index = Array.IndexOf(names, projectToAdd.Name);

                        _projects.Insert(index, projectToAdd);
                    });
                }
            }

            var projectsToKeep = e.Projects.Select(project => project.Id).ToArray();
            var projectsToRemove = _projects.Where(project => !projectsToKeep.Contains(project.Id)).ToArray();

            if (_projects.Any())
            {
                _application.Dispatcher.Invoke(() =>
                {
                    _projects.RemoveRange(projectsToRemove);
                });
            }

            _trie = new SuffixTrie<IViewProjectViewModel>(3);

            foreach (var project in _projects)
            {
                _trie.Add(project.Name.ToLowerInvariant(), project);
            }

            IsErrored = false;
            IsBusy = false;

            NotifyOfPropertyChange(() => HasProjects);
            NotifyOfPropertyChange(() => HasNoProjects);
            NotifyOfPropertyChange(() => IsViewable);
        }

        private bool ShouldExitHandler(BuildTrackerConnectionEventArgs e)
        {
            return SettingsId != e.SettingsId;
        }
    }
}