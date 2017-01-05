// <copyright file="ConnectionViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.ViewModels.Factories
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;
    using WPF.ViewModels.Factories;

    /// <summary>
    /// The <see cref="ConnectionViewModelFactory" /> class.
    /// </summary>
    public class ConnectionViewModelFactory : IConnectionViewModelFactory
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IBuildMonitor _buildMonitor;
        private readonly IEditConnectionViewModelFactory<ConnectionSettings> _editConnectionFactory;
        private readonly IProjectViewModelFactory _projectFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionViewModelFactory" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildMonitor">The build monitor.</param>
        /// <param name="projectFactory">The project factory.</param>
        /// <param name="editConnectionFactory">The edit connection factory.</param>
        public ConnectionViewModelFactory(IEventAggregator eventAggregator, IBuildMonitor buildMonitor, IProjectViewModelFactory projectFactory, IEditConnectionViewModelFactory<ConnectionSettings> editConnectionFactory)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildMonitor).IsNotNull();
            Ensure.That(projectFactory).IsNotNull();
            Ensure.That(editConnectionFactory).IsNotNull();

            _eventAggregator = eventAggregator;
            _buildMonitor = buildMonitor;
            _projectFactory = projectFactory;
            _editConnectionFactory = editConnectionFactory;
        }

        /// <summary>
        /// Gets the type the factory applies to.
        /// </summary>
        /// <value>
        /// The type the factory applies to.
        /// </value>
        public Type AppliesTo { get; } = typeof(ConnectionSettings);

        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        public WPF.ViewModels.ConnectionViewModel Create(Settings.ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            return new ConnectionViewModel(_eventAggregator, _buildMonitor, _projectFactory, _editConnectionFactory, settings.Id)
            {
                SettingsName = settings.Name
            };
        }
    }
}
