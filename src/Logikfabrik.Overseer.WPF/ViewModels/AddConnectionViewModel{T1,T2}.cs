// <copyright file="AddConnectionViewModel{T1,T2}.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Caliburn.Micro;
    using EnsureThat;
    using Factories;
    using JetBrains.Annotations;
    using Navigation;
    using Overseer.Logging;
    using Settings;

    /// <summary>
    /// The <see cref="AddConnectionViewModel{T1,T2}" /> class.
    /// </summary>
    /// <typeparam name="T1">The <see cref="ConnectionSettings" /> type.</typeparam>
    /// <typeparam name="T2">The <see cref="EditConnectionSettingsViewModel{T}" /> type.</typeparam>
    // ReSharper disable once InheritdocConsiderUsage
    public class AddConnectionViewModel<T1, T2> : ViewModel
        where T1 : ConnectionSettings
        where T2 : EditConnectionSettingsViewModel<T1>, new()
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogService _logService;
        private readonly IConnectionSettingsRepository _connectionSettingsRepository;
        private readonly IBuildProviderStrategy _buildProviderStrategy;
        private readonly IEditTrackedProjectViewModelFactory _editTrackedProjectViewModelFactory;
        private readonly IEditTrackedProjectsViewModelFactory _editTrackedProjectsViewModelFactory;
        private readonly INavigationMessageFactory<ViewConnectionsViewModel> _viewConnectionsNavigationMessageFactory;
        private readonly INavigationMessageFactory<NewConnectionViewModel> _newConnectionNavigationMessageFactory;
        private INotifyTask _connectionTask;
        private bool _hasConnected;
        private EditConnectionSettingsViewModel<T1> _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionViewModel{T1,T2}" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="logService">The log service.</param>
        /// <param name="connectionSettingsRepository">The connection settings repository.</param>
        /// <param name="buildProviderStrategy">The build provider strategy.</param>
        /// <param name="editTrackedProjectViewModelFactory">The edit tracked project view model factory.</param>
        /// <param name="editTrackedProjectsViewModelFactory">The edit tracked projects view model factory.</param>
        /// <param name="editConnectionSettingsFactory">The edit connection settings factory.</param>
        /// <param name="viewConnectionsNavigationMessageFactory">The view connections navigation message factory.</param>
        /// <param name="newConnectionNavigationMessageFactory">The new connection navigation message factory.</param>
        [UsedImplicitly]

        // ReSharper disable once InheritdocConsiderUsage
        public AddConnectionViewModel(
            IPlatformProvider platformProvider,
            IEventAggregator eventAggregator,
            ILogService logService,
            IConnectionSettingsRepository connectionSettingsRepository,
            IBuildProviderStrategy buildProviderStrategy,
            IEditTrackedProjectViewModelFactory editTrackedProjectViewModelFactory,
            IEditTrackedProjectsViewModelFactory editTrackedProjectsViewModelFactory,
            IEditConnectionSettingsViewModelFactory<T1, T2> editConnectionSettingsFactory,
            INavigationMessageFactory<ViewConnectionsViewModel> viewConnectionsNavigationMessageFactory,
            INavigationMessageFactory<NewConnectionViewModel> newConnectionNavigationMessageFactory)
            : base(platformProvider)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(logService).IsNotNull();
            Ensure.That(connectionSettingsRepository).IsNotNull();
            Ensure.That(buildProviderStrategy).IsNotNull();
            Ensure.That(editTrackedProjectViewModelFactory).IsNotNull();
            Ensure.That(editTrackedProjectsViewModelFactory).IsNotNull();
            Ensure.That(editConnectionSettingsFactory).IsNotNull();
            Ensure.That(viewConnectionsNavigationMessageFactory).IsNotNull();
            Ensure.That(newConnectionNavigationMessageFactory).IsNotNull();

            _eventAggregator = eventAggregator;
            _logService = logService;
            _connectionSettingsRepository = connectionSettingsRepository;
            _buildProviderStrategy = buildProviderStrategy;
            _editTrackedProjectViewModelFactory = editTrackedProjectViewModelFactory;
            _editTrackedProjectsViewModelFactory = editTrackedProjectsViewModelFactory;
            _viewConnectionsNavigationMessageFactory = viewConnectionsNavigationMessageFactory;
            _newConnectionNavigationMessageFactory = newConnectionNavigationMessageFactory;

            _connectionTask = new NotifyTask();

            Settings = editConnectionSettingsFactory.Create();
            Settings.BuildsPerProject = 5;
            DisplayName = Properties.Resources.AddConnection_View;
        }

        /// <summary>
        /// Gets the connection task.
        /// </summary>
        /// <value>
        /// The connection task.
        /// </value>
        public INotifyTask ConnectionTask
        {
            get
            {
                return _connectionTask;
            }

            private set
            {
                _connectionTask = value;
                NotifyOfPropertyChange(() => ConnectionTask);
            }
        }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public EditConnectionSettingsViewModel<T1> Settings
        {
            get
            {
                return _settings;
            }

            protected set
            {
                Ensure.That(value).IsNotNull();

                if (_settings != null)
                {
                    _settings.PropertyChanged -= SettingsPropertyChanged;
                }

                _settings = value;

                _settings.PropertyChanged += SettingsPropertyChanged;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the settings for this instance have been used to successfully connect.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the settings for this instance have been used to successfully connect; otherwise, <c>false</c>.
        /// </value>
        public bool HasConnected
        {
            get
            {
                return _hasConnected;
            }

            private set
            {
                _hasConnected = value;
                NotifyOfPropertyChange(() => HasConnected);
                NotifyOfPropertyChange(() => IsValidAndHasConnected);
                NotifyOfPropertyChange(() => IsValidAndHasNotConnected);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the settings for this instance are valid and have been used to successfully connect.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the settings for this instance are valid and have been used to successfully connect; otherwise, <c>false</c>.
        /// </value>
        public bool IsValidAndHasConnected => Settings.IsValid && HasConnected;

        /// <summary>
        /// Gets a value indicating whether the settings for this instance are valid, but have not been used to successfully connect.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the settings for this instance are valid, but have not been used to successfully connect; otherwise, <c>false</c>.
        /// </value>
        public bool IsValidAndHasNotConnected => Settings.IsValid && !HasConnected;

        /// <summary>
        /// Tries to connect.
        /// </summary>
        [UsedImplicitly]
        public void TryConnect()
        {
            if (!Settings.IsValid || HasConnected)
            {
                return;
            }

            ConnectionTask = new NotifyTask(Connect());
        }

        /// <summary>
        /// Adds the connection and closes the view.
        /// </summary>
        public void Add()
        {
            if (!IsValidAndHasConnected)
            {
                return;
            }

            _connectionSettingsRepository.Add(Settings.GetSettings());

            if (string.IsNullOrWhiteSpace(Context))
            {
                var message = _viewConnectionsNavigationMessageFactory.Create();

                _eventAggregator.PublishOnUIThread(message);
            }

            TryClose();
        }

        public void Cancel()
        {
            if (string.IsNullOrWhiteSpace(Context))
            {
                var message = _newConnectionNavigationMessageFactory.Create();

                _eventAggregator.PublishOnUIThread(message);
            }

            TryClose();
        }

        private async Task Connect()
        {
            try
            {
                var candidateSettings = Settings.GetSettings();

                var provider = _buildProviderStrategy.Create(candidateSettings);

                var projects = await provider.GetProjectsAsync(CancellationToken.None).ConfigureAwait(false);

                Settings.IsDirty = false;
                Settings.TrackedProjects = _editTrackedProjectsViewModelFactory.Create(projects.OrderBy(project => project.Name).Select(project => _editTrackedProjectViewModelFactory.Create(project.Id, project.Name, true)).ToArray());

                HasConnected = true;
            }
            catch (Exception ex)
            {
                _logService.Log(GetType(), new LogEntry(LogEntryType.Error, "An error occurred while adding connection.", ex));

                HasConnected = false;

                throw;
            }
        }

        private void SettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Settings.IsDirty))
            {
                HasConnected = false;

                Settings.TrackedProjects = null;
            }

            if (e.PropertyName == nameof(Settings.IsValid))
            {
                NotifyOfPropertyChange(() => IsValidAndHasConnected);
                NotifyOfPropertyChange(() => IsValidAndHasNotConnected);
            }
        }
    }
}