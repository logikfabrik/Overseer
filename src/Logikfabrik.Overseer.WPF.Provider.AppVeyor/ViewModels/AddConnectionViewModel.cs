// <copyright file="AddConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.ViewModels
{
    using Caliburn.Micro;
    using Settings;

    /// <summary>
    /// The <see cref="AddConnectionViewModel" /> class.
    /// </summary>
    public class AddConnectionViewModel : WPF.ViewModels.AddConnectionViewModel<AppVeyor.ConnectionSettings>
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
            _settings = new ConnectionSettingsViewModel();
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
        public override string ViewName { get; } = "Add AppVeyor connection";

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns>
        /// The settings.
        /// </returns>
        protected override AppVeyor.ConnectionSettings GetSettings()
        {
            return new AppVeyor.ConnectionSettings
            {
                Name = _settings.Name,
                Token = _settings.Token
            };
        }
    }
}
