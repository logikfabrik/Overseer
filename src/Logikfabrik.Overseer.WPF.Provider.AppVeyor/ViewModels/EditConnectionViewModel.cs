// <copyright file="EditConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using Factories;
    using Settings;

    /// <summary>
    /// The <see cref="EditConnectionViewModel" /> class.
    /// </summary>
    public class EditConnectionViewModel : WPF.ViewModels.EditConnectionViewModel<AppVeyor.ConnectionSettings>
    {
        private readonly ConnectionSettingsViewModel _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="settingsRepository">The build provider settings repository.</param>
        /// <param name="connectionSettingsFactory">The connection settings factory.</param>
        /// <param name="currentSettings">The current settings.</param>
        public EditConnectionViewModel(IEventAggregator eventAggregator, IConnectionSettingsRepository settingsRepository, IConnectionSettingsViewModelFactory connectionSettingsFactory, AppVeyor.ConnectionSettings currentSettings)
            : base(eventAggregator, settingsRepository, currentSettings)
        {
            Ensure.That(connectionSettingsFactory).IsNotNull();

            _settings = connectionSettingsFactory.Create();

            _settings.Name = currentSettings.Name;
            _settings.Token = currentSettings.Token;
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public override WPF.ViewModels.ConnectionSettingsViewModel<AppVeyor.ConnectionSettings> Settings => _settings;
    }
}
