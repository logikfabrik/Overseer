// <copyright file="EditConnectionViewModel{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Caliburn.Micro;
    using EnsureThat;
    using Factories;
    using Settings;

    /// <summary>
    /// The <see cref="EditConnectionViewModel{T}" /> class. Base class for view models for editing connections.
    /// </summary>
    /// <typeparam name="T">The <see cref="ConnectionSettings" /> type.</typeparam>
    public abstract class EditConnectionViewModel<T> : ViewModel
        where T : ConnectionSettings
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IConnectionSettingsRepository _settingsRepository;
        private readonly IBuildProviderFactory _buildProviderFactory;
        private readonly IProjectToMonitorViewModelFactory _projectToMonitorFactory;
        private readonly IProjectsToMonitorViewModelFactory _projectsToMonitorFactory;
        private readonly T _currentSettings;
        private INotifyTask _connectionTask;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionViewModel{T}" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="settingsRepository">The settings repository.</param>
        /// <param name="buildProviderFactory">The build provider factory.</param>
        /// <param name="projectToMonitorFactory">The project to monitor factory.</param>
        /// <param name="projectsToMonitorFactory">The projects to monitor factory.</param>
        /// <param name="currentSettings">The current settings.</param>
        protected EditConnectionViewModel(
            IEventAggregator eventAggregator,
            IConnectionSettingsRepository settingsRepository,
            IBuildProviderFactory buildProviderFactory,
            IProjectToMonitorViewModelFactory projectToMonitorFactory,
            IProjectsToMonitorViewModelFactory projectsToMonitorFactory,
            T currentSettings)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(settingsRepository).IsNotNull();
            Ensure.That(buildProviderFactory).IsNotNull();
            Ensure.That(projectToMonitorFactory).IsNotNull();
            Ensure.That(projectsToMonitorFactory).IsNotNull();
            Ensure.That(currentSettings).IsNotNull();

            _eventAggregator = eventAggregator;
            _settingsRepository = settingsRepository;
            _buildProviderFactory = buildProviderFactory;
            _projectToMonitorFactory = projectToMonitorFactory;
            _projectsToMonitorFactory = projectsToMonitorFactory;
            _currentSettings = currentSettings;

            _connectionTask = new NotifyTask(Connect(_currentSettings));
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
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public abstract ConnectionSettingsViewModel<T> Settings { get; }

        /// <summary>
        /// Gets the view name.
        /// </summary>
        /// <value>
        /// The view name.
        /// </value>
        public override string ViewName { get; } = "Edit connection";

        /// <summary>
        /// Try the connection.
        /// </summary>
        public void TryConnection()
        {
            if (Settings.IsNotDirty || !Settings.Validator.Validate(Settings).IsValid)
            {
                return;
            }

            ConnectionTask = new NotifyTask(Connect(Settings.GetSettings()));
        }

        /// <summary>
        /// Edit the connection.
        /// </summary>
        public void EditConnection()
        {
            if (Settings.IsDirty || !Settings.Validator.Validate(Settings).IsValid)
            {
                return;
            }

            Settings.UpdateSettings(_currentSettings);

            _settingsRepository.Update(_currentSettings);

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

        private async Task Connect(T settings)
        {
            using (var provider = _buildProviderFactory.Create(settings))
            {
                var projects = await provider.GetProjectsAsync(CancellationToken.None).ConfigureAwait(false);

                Settings.ProjectsToMonitor = _projectsToMonitorFactory.CreateProjectsToMonitorViewModel(projects.OrderBy(project => project.Name).Select(project => _projectToMonitorFactory.CreateProjectToMonitorViewModel(project.Id, project.Name, settings.ProjectsToMonitor.Contains(project.Id))));
                Settings.IsDirty = false;
            }
        }
    }
}
