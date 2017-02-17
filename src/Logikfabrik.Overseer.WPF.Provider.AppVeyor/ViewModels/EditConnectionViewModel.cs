// <copyright file="EditConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using Settings;
    using WPF.ViewModels.Factories;

    /// <summary>
    /// The <see cref="EditConnectionViewModel" /> class.
    /// </summary>
    public class EditConnectionViewModel : WPF.ViewModels.EditConnectionViewModel<AppVeyor.ConnectionSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="settingsRepository">The build provider settings repository.</param>
        /// <param name="connectionSettingsFactory">The connection settings factory.</param>
        /// <param name="projectToMonitorFactory">The project to monitor factory.</param>
        /// <param name="projectsToMonitorFactory">The projects to monitor factory.</param>
        /// <param name="currentSettings">The current settings.</param>
        public EditConnectionViewModel(
            IEventAggregator eventAggregator,
            IConnectionSettingsRepository settingsRepository,
            IConnectionSettingsViewModelFactory<AppVeyor.ConnectionSettings, ConnectionSettingsViewModel> connectionSettingsFactory,
            IProjectToMonitorViewModelFactory projectToMonitorFactory,
            IProjectsToMonitorViewModelFactory projectsToMonitorFactory,
            AppVeyor.ConnectionSettings currentSettings)
            : base(
                  eventAggregator,
                  settingsRepository,
                  projectToMonitorFactory,
                  projectsToMonitorFactory,
                  currentSettings)
        {
            Ensure.That(connectionSettingsFactory).IsNotNull();

            var settings = connectionSettingsFactory.Create();

            settings.Name = currentSettings.Name;
            settings.Token = currentSettings.Token;
            settings.IsDirty = false;

            Settings = settings;
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
