// <copyright file="AddConnectionViewModel{T}.cs" company="Logikfabrik">
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
    /// The <see cref="AddConnectionViewModel{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The <see cref="ConnectionSettings" /> type.</typeparam>
    public abstract class AddConnectionViewModel<T> : ViewModel
        where T : ConnectionSettings
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IConnectionSettingsRepository _settingsRepository;
        private readonly IProjectToMonitorViewModelFactory _projectToMonitorFactory;
        private readonly IProjectsToMonitorViewModelFactory _projectsToMonitorFactory;
        private INotifyTask _connectionTask;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionViewModel{T}" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="settingsRepository">The settings repository.</param>
        /// <param name="projectToMonitorFactory">The project to monitor factory.</param>
        protected AddConnectionViewModel(IEventAggregator eventAggregator, IConnectionSettingsRepository settingsRepository, IProjectToMonitorViewModelFactory projectToMonitorFactory, IProjectsToMonitorViewModelFactory projectsToMonitorFactory)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(settingsRepository).IsNotNull();
            Ensure.That(projectToMonitorFactory).IsNotNull();
            Ensure.That(projectsToMonitorFactory).IsNotNull();

            _eventAggregator = eventAggregator;
            _settingsRepository = settingsRepository;
            _projectToMonitorFactory = projectToMonitorFactory;
            _projectsToMonitorFactory = projectsToMonitorFactory;

            _connectionTask = new NotifyTask();
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
        public override string ViewName { get; } = "Add connection";

        /// <summary>
        /// Try the connection.
        /// </summary>
        public void TryConnection()
        {
            if (Settings.IsNotDirty || !Settings.Validator.Validate(Settings).IsValid)
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
            if (Settings.IsDirty || !Settings.Validator.Validate(Settings).IsValid)
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
            var candidateSettings = Settings.GetSettings();

            using (var provider = BuildProviderFactory.GetProvider(candidateSettings))
            {
                var projects = await provider.GetProjectsAsync(CancellationToken.None).ConfigureAwait(false);

                Settings.ProjectsToMonitor = _projectsToMonitorFactory.Create(projects.OrderBy(project => project.Name).Select(project => _projectToMonitorFactory.Create(project, true)));
                Settings.IsDirty = false;
            }
        }
    }
}