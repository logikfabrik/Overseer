// <copyright file="AddConnectionViewModel{T}.cs" company="Logikfabrik">
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

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionViewModel{T}" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="settingsRepository">The settings repository.</param>
        protected AddConnectionViewModel(IEventAggregator eventAggregator, IConnectionSettingsRepository settingsRepository)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(settingsRepository).IsNotNull();

            _eventAggregator = eventAggregator;
            _settingsRepository = settingsRepository;
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

                    Settings.ProjectsToMonitor = projects.Select(project => new ProjectToMonitorViewModel(project.Name, project.Id, true)).ToArray();
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
    }
}