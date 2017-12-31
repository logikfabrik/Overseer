// <copyright file="AddConnectionViewModel{T1,T2}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
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
    using Navigation;
    using Overseer.Logging;
    using Settings;

    /// <summary>
    /// The <see cref="AddConnectionViewModel{T1,T2}" /> class.
    /// </summary>
    /// <typeparam name="T1">The <see cref="ConnectionSettings" /> type.</typeparam>
    /// <typeparam name="T2">The <see cref="ConnectionSettingsViewModel{T}" /> type.</typeparam>
    // ReSharper disable once InheritdocConsiderUsage
    public class AddConnectionViewModel<T1, T2> : ViewModel
        where T1 : ConnectionSettings
        where T2 : ConnectionSettingsViewModel<T1>, new()
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogService _logService;
        private readonly IConnectionSettingsRepository _settingsRepository;
        private readonly IBuildProviderStrategy _buildProviderStrategy;
        private readonly ITrackedProjectViewModelFactory _trackedProjectFactory;
        private readonly ITrackedProjectsViewModelFactory _trackedProjectsFactory;
        private INotifyTask _connectionTask;
        private bool _hasConnected;
        private ConnectionSettingsViewModel<T1> _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionViewModel{T1,T2}" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="logService">The log service.</param>
        /// <param name="settingsRepository">The settings repository.</param>
        /// <param name="buildProviderStrategy">The build provider strategy.</param>
        /// <param name="trackedProjectFactory">The tracked project factory.</param>
        /// <param name="trackedProjectsFactory">The tracked projects factory.</param>
        /// <param name="connectionSettingsFactory">The settings factory.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public AddConnectionViewModel(
            IPlatformProvider platformProvider,
            IEventAggregator eventAggregator,
            ILogService logService,
            IConnectionSettingsRepository settingsRepository,
            IBuildProviderStrategy buildProviderStrategy,
            ITrackedProjectViewModelFactory trackedProjectFactory,
            ITrackedProjectsViewModelFactory trackedProjectsFactory,
            IConnectionSettingsViewModelFactory<T1, T2> connectionSettingsFactory)
            : base(platformProvider)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(logService).IsNotNull();
            Ensure.That(settingsRepository).IsNotNull();
            Ensure.That(buildProviderStrategy).IsNotNull();
            Ensure.That(trackedProjectFactory).IsNotNull();
            Ensure.That(trackedProjectsFactory).IsNotNull();
            Ensure.That(connectionSettingsFactory).IsNotNull();

            _eventAggregator = eventAggregator;
            _logService = logService;
            _settingsRepository = settingsRepository;
            _buildProviderStrategy = buildProviderStrategy;
            _trackedProjectFactory = trackedProjectFactory;
            _trackedProjectsFactory = trackedProjectsFactory;

            _connectionTask = new NotifyTask();

            Settings = connectionSettingsFactory.Create();
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
        public ConnectionSettingsViewModel<T1> Settings
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
        public void TryConnect()
        {
            if (!Settings.IsValid || HasConnected)
            {
                return;
            }

            ConnectionTask = new NotifyTask(Connect());
        }

        /// <summary>
        /// Adds the connection.
        /// </summary>
        public void Add()
        {
            if (!IsValidAndHasConnected)
            {
                return;
            }

            _settingsRepository.Add(Settings.GetSettings());

            var message = new NavigationMessage(typeof(ConnectionsViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }

        private async Task Connect()
        {
            try
            {
                var candidateSettings = Settings.GetSettings();

                var provider = _buildProviderStrategy.Create(candidateSettings);

                var projects = await provider.GetProjectsAsync(CancellationToken.None).ConfigureAwait(false);

                Settings.IsDirty = false;
                Settings.TrackedProjects = _trackedProjectsFactory.Create(projects.OrderBy(project => project.Name).Select(project => _trackedProjectFactory.Create(project.Id, project.Name, true)).ToArray());

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