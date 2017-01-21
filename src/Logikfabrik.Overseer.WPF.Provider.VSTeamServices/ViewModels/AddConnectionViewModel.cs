// <copyright file="AddConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.ViewModels
{
    using Caliburn.Micro;
    using Settings;

    /// <summary>
    /// The <see cref="AddConnectionViewModel" /> class.
    /// </summary>
    public class AddConnectionViewModel : WPF.ViewModels.AddConnectionViewModel<VSTeamServices.ConnectionSettings>
    {
        private readonly ConnectionSettingsViewModel _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="settingsRepository">The settings repository.</param>
        public AddConnectionViewModel(IEventAggregator eventAggregator, IConnectionSettingsRepository settingsRepository)
            : base(eventAggregator, settingsRepository)
        {
            _settings = new ConnectionSettingsViewModel
            {
                Url = "https://"
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
        /// Gets the settings.
        /// </summary>
        /// <returns>
        /// The settings.
        /// </returns>
        protected override VSTeamServices.ConnectionSettings GetSettings()
        {
            return new VSTeamServices.ConnectionSettings
            {
                Name = _settings.Name,
                Url = _settings.Url,
                Token = _settings.Token
            };
        }
    }
}