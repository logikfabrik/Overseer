// <copyright file="ConnectionViewModel{T}.cs" company="Logikfabrik">
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
    /// The <see cref="ConnectionViewModel{T}" /> class. Base view model for CI connections.
    /// </summary>
    /// <typeparam name="T">The <see cref="ConnectionSettings" /> type.</typeparam>
    // ReSharper disable once InheritdocConsiderUsage
    public class ConnectionViewModel<T> : ViewModel, IConnectionViewModel
        where T : ConnectionSettings
    {
        private readonly IApp _application;
        private readonly IEventAggregator _eventAggregator;
        private readonly IProjectViewModelFactory _projectFactory;
        private readonly IRemoveConnectionViewModelFactory _removeConnectionFactory;
        private readonly IEditConnectionViewModelFactory<T> _editConnectionFactory;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly CollectionViewSource _filteredProjects;
        private readonly BindableCollection<IProjectViewModel> _projects;
        private SuffixTrie<IProjectViewModel> _trie;
        private string _filter;
        private IEnumerable<IProjectViewModel> _matches;
        private bool _isBusy;
        private bool _isErrored;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionViewModel{T}" /> class.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildTracker">The build tracker.</param>
        /// <param name="projectFactory">The project factory.</param>
        /// <param name="removeConnectionFactory">The remove connection factory.</param>
        /// <param name="editConnectionFactory">The edit connection factory.</param>
        /// <param name="settings">The settings.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public ConnectionViewModel(
            IApp application,
            IPlatformProvider platformProvider,
            IEventAggregator eventAggregator,
            IBuildTracker buildTracker,
            IProjectViewModelFactory projectFactory,
            IRemoveConnectionViewModelFactory removeConnectionFactory,
            IEditConnectionViewModelFactory<T> editConnectionFactory,
            T settings)
            : base(platformProvider)
        {
            Ensure.That(application).IsNotNull();
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildTracker).IsNotNull();
            Ensure.That(projectFactory).IsNotNull();
            Ensure.That(removeConnectionFactory).IsNotNull();
            Ensure.That(editConnectionFactory).IsNotNull();
            Ensure.That(settings).IsNotNull();

            _application = application;
            _eventAggregator = eventAggregator;
            _projectFactory = projectFactory;
            _removeConnectionFactory = removeConnectionFactory;
            _editConnectionFactory = editConnectionFactory;
            Settings = settings;
            _isBusy = true;
            _isErrored = false;
            DisplayName = Properties.Resources.Connection_View;
            KeepAlive = true;

            WeakEventManager<IBuildTracker, BuildTrackerConnectionErrorEventArgs>.AddHandler(buildTracker, nameof(buildTracker.ConnectionError), BuildTrackerConnectionError);
            WeakEventManager<IBuildTracker, BuildTrackerConnectionProgressEventArgs>.AddHandler(buildTracker, nameof(buildTracker.ConnectionProgressChanged), BuildTrackerConnectionProgressChanged);

            _projects = new BindableCollection<IProjectViewModel>();

            _filteredProjects = new CollectionViewSource
            {
                Source = _projects
            };

            _filteredProjects.Filter += (sender, e) =>
            {
                var project = (IProjectViewModel)e.Item;

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
            var item = _editConnectionFactory.Create(Settings);

            var message = new NavigationMessage(item);

            _eventAggregator.PublishOnUIThread(message);
        }

        /// <inheritdoc />
        public void Remove()
        {
            var item = _removeConnectionFactory.Create(this);

            var message = new NavigationMessage(item);

            _eventAggregator.PublishOnUIThread(message);
        }

        /// <inheritdoc />
        public void View()
        {
            var message = new NavigationMessage(this);

            _eventAggregator.PublishOnUIThread(message);
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
                        var projectToAdd = _projectFactory.Create(SettingsId, project.Id, project.Name);

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

            _trie = new SuffixTrie<IProjectViewModel>(3);

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