// <copyright file="EditConnectionViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.ViewModels.Factories
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;
    using Settings;
    using WPF.ViewModels;
    using WPF.ViewModels.Factories;

    /// <summary>
    /// The <see cref="EditConnectionViewModelFactory" /> class.
    /// </summary>
    public sealed class EditConnectionViewModelFactory : IEditConnectionViewModelFactory<VSTeamServices.ConnectionSettings>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IConnectionSettingsRepository _settingsRepository;
        private readonly IConnectionSettingsViewModelFactory _connectionSettingsFactory;
        private readonly IProjectToMonitorViewModelFactory _projectToMonitorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionViewModelFactory" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="settingsRepository">The settings repository.</param>
        /// <param name="connectionSettingsFactory">The connection settings factory.</param>
        /// <param name="projectToMonitorFactory">The project to monitor factory.</param>
        public EditConnectionViewModelFactory(IEventAggregator eventAggregator, IConnectionSettingsRepository settingsRepository, IConnectionSettingsViewModelFactory connectionSettingsFactory, IProjectToMonitorViewModelFactory projectToMonitorFactory)
        {
            // TODO: Make generic.
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(settingsRepository).IsNotNull();
            Ensure.That(connectionSettingsFactory).IsNotNull();
            Ensure.That(projectToMonitorFactory).IsNotNull();

            _eventAggregator = eventAggregator;
            _settingsRepository = settingsRepository;
            _connectionSettingsFactory = connectionSettingsFactory;
            _projectToMonitorFactory = projectToMonitorFactory;
        }

        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        public EditConnectionViewModel<VSTeamServices.ConnectionSettings> Create(Guid settingsId)
        {
            Ensure.That(settingsId).IsNotEmpty();

            var settings = _settingsRepository.Get(settingsId) as VSTeamServices.ConnectionSettings;

            return new EditConnectionViewModel(_eventAggregator, _settingsRepository, _connectionSettingsFactory, _projectToMonitorFactory, settings);
        }
    }
}
