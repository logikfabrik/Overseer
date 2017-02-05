// <copyright file="EditConnectionViewModel{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Linq;
    using System.Net.Http;
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
        private readonly IProjectToMonitorViewModelFactory _projectToMonitorFactory;
        private readonly T _currentSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionViewModel{T}" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="settingsRepository">The settings repository.</param>
        /// <param name="projectToMonitorFactory">The project to monitor factory.</param>
        /// <param name="currentSettings">The current settings.</param>
        protected EditConnectionViewModel(IEventAggregator eventAggregator, IConnectionSettingsRepository settingsRepository, IProjectToMonitorViewModelFactory projectToMonitorFactory, T currentSettings)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(settingsRepository).IsNotNull();
            Ensure.That(projectToMonitorFactory).IsNotNull();
            Ensure.That(currentSettings).IsNotNull();

            _eventAggregator = eventAggregator;
            _settingsRepository = settingsRepository;
            _projectToMonitorFactory = projectToMonitorFactory;
            _currentSettings = currentSettings;
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
        /// Tries the connection.
        /// </summary>
        /// <returns>A task.</returns>
        public async Task TryConnection()
        {
            if (Settings.IsNotDirty || !Settings.Validator.Validate(Settings).IsValid)
            {
                return;
            }

            var candidateSettings = Settings.GetSettings();

            using (var provider = BuildProviderFactory.GetProvider(candidateSettings))
            {
                try
                {
                    var projects = await provider.GetProjectsAsync(CancellationToken.None).ConfigureAwait(false);

                    Settings.ProjectsToMonitor = projects.Select(project => _projectToMonitorFactory.Create(project, _currentSettings.ProjectsToMonitor.Contains(project.Id))).ToArray();
                    Settings.IsDirty = false;
                }
                catch (HttpRequestException)
                {
                    // TODO: Show error message; the connection failed. And log.
                }
                catch (Exception)
                {
                    // TODO: Show warning message; the connection failed. And log.
                    Settings.IsDirty = false;
                }
            }
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
    }
}
