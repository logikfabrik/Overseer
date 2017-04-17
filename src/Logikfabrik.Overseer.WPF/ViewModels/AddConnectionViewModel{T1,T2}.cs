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
    using Settings;

    /// <summary>
    /// The <see cref="AddConnectionViewModel{T1,T2}" /> class.
    /// </summary>
    /// <typeparam name="T1">The <see cref="ConnectionSettings" /> type.</typeparam>
    /// <typeparam name="T2">The <see cref="ConnectionSettingsViewModel{T}" /> type.</typeparam>
    public class AddConnectionViewModel<T1, T2> : ViewModel
        where T1 : ConnectionSettings
        where T2 : ConnectionSettingsViewModel<T1>, new()
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IConnectionSettingsRepository _settingsRepository;
        private readonly IBuildProviderStrategy _buildProviderStrategy;
        private readonly IProjectToMonitorViewModelFactory _projectToMonitorFactory;
        private readonly IProjectsToMonitorViewModelFactory _projectsToMonitorFactory;
        private INotifyTask _connectionTask;
        private bool _hasConnected;
        private ConnectionSettingsViewModel<T1> _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionViewModel{T1,T2}" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="settingsRepository">The settings repository.</param>
        /// <param name="buildProviderStrategy">The build provider strategy.</param>
        /// <param name="projectToMonitorFactory">The project to monitor factory.</param>
        /// <param name="projectsToMonitorFactory">The projects to monitor factory.</param>
        /// <param name="connectionSettingsFactory">The settings factory.</param>
        public AddConnectionViewModel(
            IEventAggregator eventAggregator,
            IConnectionSettingsRepository settingsRepository,
            IBuildProviderStrategy buildProviderStrategy,
            IProjectToMonitorViewModelFactory projectToMonitorFactory,
            IProjectsToMonitorViewModelFactory projectsToMonitorFactory,
            IConnectionSettingsViewModelFactory<T1, T2> connectionSettingsFactory)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(settingsRepository).IsNotNull();
            Ensure.That(buildProviderStrategy).IsNotNull();
            Ensure.That(projectToMonitorFactory).IsNotNull();
            Ensure.That(projectsToMonitorFactory).IsNotNull();
            Ensure.That(connectionSettingsFactory).IsNotNull();

            _eventAggregator = eventAggregator;
            _settingsRepository = settingsRepository;
            _buildProviderStrategy = buildProviderStrategy;
            _projectToMonitorFactory = projectToMonitorFactory;
            _projectsToMonitorFactory = projectsToMonitorFactory;

            _connectionTask = new NotifyTask();

            Settings = connectionSettingsFactory.Create();
            DisplayName = "Add connection";
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
                NotifyOfPropertyChange(() => HasNotConnected);
                NotifyOfPropertyChange(() => IsValidAndHasConnected);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the settings for this instance have not been used to successfully connect.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the settings for this instance have not been used to successfully connect; otherwise, <c>false</c>.
        /// </value>
        public bool HasNotConnected => !HasConnected;

        /// <summary>
        /// Gets a value indicating whether the settings for this instance are valid and have been used to successfully connect.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the settings for this instance are valid and have been used to successfully connect; otherwise, <c>false</c>.
        /// </value>
        public bool IsValidAndHasConnected => Settings.IsValid && HasConnected;

        /// <summary>
        /// Try the connection.
        /// </summary>
        public void TryConnection()
        {
            if (!Settings.IsValid || HasConnected)
            {
                return;
            }

            ConnectionTask = new NotifyTask(Connect());
        }

        /// <summary>
        /// Add the connection.
        /// </summary>
        public void AddConnection()
        {
            if (!IsValidAndHasConnected)
            {
                return;
            }

            _settingsRepository.Add(Settings.GetSettings());

            ViewConnections();
        }

        /// <summary>
        /// View the connections.
        /// </summary>
        public void ViewConnections()
        {
            var message = new NavigationMessage(typeof(ConnectionsViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }

        private async Task Connect()
        {
            try
            {
                var candidateSettings = Settings.GetSettings();

                using (var provider = _buildProviderStrategy.Create(candidateSettings))
                {
                    var projects = await provider.GetProjectsAsync(CancellationToken.None).ConfigureAwait(false);

                    Settings.IsDirty = false;
                    Settings.ProjectsToMonitor = _projectsToMonitorFactory.Create(projects.OrderBy(project => project.Name).Select(project => _projectToMonitorFactory.Create(project.Id, project.Name, true)).ToArray());

                    HasConnected = true;
                }
            }
            catch (Exception)
            {
                HasConnected = false;

                throw;
            }
        }

        private void SettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Settings.IsDirty))
            {
                HasConnected = false;

                Settings.ProjectsToMonitor = null;
            }

            if (e.PropertyName == nameof(Settings.IsValid))
            {
                NotifyOfPropertyChange(() => IsValidAndHasConnected);
            }
        }
    }
}