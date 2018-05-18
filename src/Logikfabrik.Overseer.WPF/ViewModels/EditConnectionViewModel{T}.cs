// <copyright file="EditConnectionViewModel{T}.cs" company="Logikfabrik">
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
    using Overseer.Logging;
    using Settings;

    /// <summary>
    /// The <see cref="EditConnectionViewModel{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The <see cref="ConnectionSettings" /> type.</typeparam>
    // ReSharper disable once InheritdocConsiderUsage
    public abstract class EditConnectionViewModel<T> : ViewModel
        where T : ConnectionSettings
    {
        private readonly ILogService _logService;
        private readonly IConnectionSettingsRepository _connectionSettingsRepository;
        private readonly IBuildProviderStrategy _buildProviderStrategy;
        private readonly IEditTrackedProjectViewModelFactory _editTrackedProjectViewModelFactory;
        private readonly IEditTrackedProjectsViewModelFactory _editTrackedProjectsViewModelFactory;
        private readonly T _currentSettings;
        private INotifyTask _connectionTask;
        private bool _hasConnected;
        private EditConnectionSettingsViewModel<T> _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionViewModel{T}" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="logService">The log service.</param>
        /// <param name="connectionSettingsRepository">The connection settings repository.</param>
        /// <param name="buildProviderStrategy">The build provider strategy.</param>
        /// <param name="editTrackedProjectViewModelFactory">The edit tracked project view model factory.</param>
        /// <param name="editTrackedProjectsViewModelFactory">The edit tracked projects view model factory.</param>
        /// <param name="currentSettings">The current settings.</param>
        // ReSharper disable once InheritdocConsiderUsage
        protected EditConnectionViewModel(
            IPlatformProvider platformProvider,
            ILogService logService,
            IConnectionSettingsRepository connectionSettingsRepository,
            IBuildProviderStrategy buildProviderStrategy,
            IEditTrackedProjectViewModelFactory editTrackedProjectViewModelFactory,
            IEditTrackedProjectsViewModelFactory editTrackedProjectsViewModelFactory,
            T currentSettings)
            : base(platformProvider)
        {
            Ensure.That(logService).IsNotNull();
            Ensure.That(connectionSettingsRepository).IsNotNull();
            Ensure.That(buildProviderStrategy).IsNotNull();
            Ensure.That(editTrackedProjectViewModelFactory).IsNotNull();
            Ensure.That(editTrackedProjectsViewModelFactory).IsNotNull();
            Ensure.That(currentSettings).IsNotNull();

            _logService = logService;
            _connectionSettingsRepository = connectionSettingsRepository;
            _buildProviderStrategy = buildProviderStrategy;
            _editTrackedProjectViewModelFactory = editTrackedProjectViewModelFactory;
            _editTrackedProjectsViewModelFactory = editTrackedProjectsViewModelFactory;
            _currentSettings = currentSettings;
            DisplayName = Properties.Resources.EditConnection_View;
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
        public EditConnectionSettingsViewModel<T> Settings
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

            ConnectionTask = new NotifyTask(Connect(Settings.GetSettings()));
        }

        /// <summary>
        /// Edit the connection.
        /// </summary>
        public void Edit()
        {
            if (!IsValidAndHasConnected)
            {
                return;
            }

            Settings.UpdateSettings(_currentSettings);

            _connectionSettingsRepository.Update(_currentSettings);

            TryClose();
        }

        private async Task Connect(T settings)
        {
            try
            {
                var provider = _buildProviderStrategy.Create(settings);

                var projects = await provider.GetProjectsAsync(CancellationToken.None).ConfigureAwait(false);

                Settings.IsDirty = false;
                Settings.TrackedProjects = _editTrackedProjectsViewModelFactory.Create(projects.OrderBy(project => project.Name).Select(project => _editTrackedProjectViewModelFactory.Create(project.Id, project.Name, _currentSettings.TrackedProjects.Contains(project.Id))).ToArray());

                HasConnected = true;
            }
            catch (Exception ex)
            {
                _logService.Log(GetType(), new LogEntry(LogEntryType.Error, "An error occurred while editing connection.", ex));

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
