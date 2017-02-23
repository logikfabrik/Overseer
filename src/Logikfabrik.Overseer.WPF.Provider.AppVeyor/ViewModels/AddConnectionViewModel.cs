﻿// <copyright file="AddConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using Settings;
    using WPF.ViewModels.Factories;

    /// <summary>
    /// The <see cref="AddConnectionViewModel" /> class.
    /// </summary>
    public class AddConnectionViewModel : WPF.ViewModels.AddConnectionViewModel<AppVeyor.ConnectionSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="settingsRepository">The settings repository.</param>
        /// <param name="buildProviderStrategy">The build provider strategy.</param>
        /// <param name="projectToMonitorFactory">The project to monitor factory.</param>
        /// <param name="projectsToMonitorFactory">The projects to monitor factory.</param>
        /// <param name="connectionSettingsFactory">The connection settings factory.</param>
        public AddConnectionViewModel(
            IEventAggregator eventAggregator,
            IConnectionSettingsRepository settingsRepository,
            IBuildProviderStrategy buildProviderStrategy,
            IProjectToMonitorViewModelFactory projectToMonitorFactory,
            IProjectsToMonitorViewModelFactory projectsToMonitorFactory,
            IConnectionSettingsViewModelFactory<AppVeyor.ConnectionSettings, ConnectionSettingsViewModel> connectionSettingsFactory)
            : base(
                eventAggregator,
                settingsRepository,
                buildProviderStrategy,
                projectToMonitorFactory,
                projectsToMonitorFactory)
        {
            Ensure.That(connectionSettingsFactory).IsNotNull();

            Settings = connectionSettingsFactory.Create();
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public override WPF.ViewModels.ConnectionSettingsViewModel<AppVeyor.ConnectionSettings> Settings { get; }
    }
}
