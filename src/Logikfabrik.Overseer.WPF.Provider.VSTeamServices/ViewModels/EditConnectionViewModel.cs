// <copyright file="EditConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.ViewModels
{
    using Caliburn.Micro;
    using Settings;

    /// <summary>
    /// The <see cref="EditConnectionViewModel" /> class.
    /// </summary>
    public class EditConnectionViewModel : WPF.ViewModels.EditConnectionViewModel<VSTeamServices.ConnectionSettings>
    {
        private readonly ConnectionSettingsViewModel _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="settingsRepository">The build provider settings repository.</param>
        /// <param name="currentSettings">The current settings.</param>
        public EditConnectionViewModel(IEventAggregator eventAggregator, IConnectionSettingsRepository settingsRepository, VSTeamServices.ConnectionSettings currentSettings)
            : base(eventAggregator, settingsRepository, currentSettings)
        {
            _settings = new ConnectionSettingsViewModel
            {
                Name = currentSettings.Name,
                Url = currentSettings.Url,
                Token = currentSettings.Token
            };
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public override WPF.ViewModels.ConnectionSettingsViewModel Settings => _settings;

        /// <summary>
        /// Gets the view name.
        /// </summary>
        /// <value>
        /// The view name.
        /// </value>
        public override string ViewName { get; } = "Edit VSTS connection";

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <param name="currentSettings">The current settings.</param>
        /// <returns>
        /// The settings.
        /// </returns>
        protected override VSTeamServices.ConnectionSettings GetSettings(VSTeamServices.ConnectionSettings currentSettings)
        {
            currentSettings.Name = _settings.Name;
            currentSettings.Url = _settings.Url;
            currentSettings.Token = _settings.Token;

            return currentSettings;
        }
    }
}
