// <copyright file="AddConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.ViewModels
{
    using Caliburn.Micro;
    using FluentValidation;
    using Settings;
    using Validators;

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
        public AddConnectionViewModel(IEventAggregator eventAggregator, IConnectionSettingsRepository settingsRepository)
            : base(eventAggregator, settingsRepository)
        {
            Validator = new AddConnectionViewModelValidator();
        }

        /// <summary>
        /// Gets the view name.
        /// </summary>
        /// <value>
        /// The view name.
        /// </value>
        public override string ViewName => "Add AppVeyor connection";

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; set; }

        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <value>
        /// The validator.
        /// </value>
        protected override IValidator Validator { get; }

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
                Name = ConnectionName,
                Token = Token
            };
        }
    }
}
