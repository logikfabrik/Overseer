// <copyright file="EditConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using Overseer.Logging;
    using Settings;
    using WPF.ViewModels.Factories;

    /// <summary>
    /// The <see cref="EditConnectionViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class EditConnectionViewModel : WPF.ViewModels.EditConnectionViewModel<TeamCity.ConnectionSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="logService">The log service.</param>
        /// <param name="settingsRepository">The build provider settings repository.</param>
        /// <param name="connectionSettingsFactory">The connection settings factory.</param>
        /// <param name="buildProviderStrategy">The build provider strategy.</param>
        /// <param name="trackedProjectFactory">The tracked project factory.</param>
        /// <param name="trackedProjectsFactory">The tracked projects factory.</param>
        /// <param name="currentSettings">The current settings.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public EditConnectionViewModel(
            IPlatformProvider platformProvider,
            ILogService logService,
            IConnectionSettingsRepository settingsRepository,
            IConnectionSettingsViewModelFactory<TeamCity.ConnectionSettings, ConnectionSettingsViewModel> connectionSettingsFactory,
            IBuildProviderStrategy buildProviderStrategy,
            ITrackedProjectViewModelFactory trackedProjectFactory,
            ITrackedProjectsViewModelFactory trackedProjectsFactory,
            TeamCity.ConnectionSettings currentSettings)
            : base(
                  platformProvider,
                  logService,
                  settingsRepository,
                  buildProviderStrategy,
                  trackedProjectFactory,
                  trackedProjectsFactory,
                  currentSettings)
        {
            Ensure.That(connectionSettingsFactory).IsNotNull();

            var settings = connectionSettingsFactory.Create();

            settings.Name = currentSettings.Name;
            settings.Url = currentSettings.Url;
            settings.AuthenticationType = currentSettings.AuthenticationType;
            settings.Username = currentSettings.Username;
            settings.Password = currentSettings.Password;
            settings.BuildsPerProject = currentSettings.BuildsPerProject;
            settings.IsDirty = false;

            Settings = settings;

            TryConnect();
        }
    }
}
