// <copyright file="EditConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.ViewModels
{
    using System;
    using System.Linq;
    using Caliburn.Micro;
    using FluentValidation;
    using Settings;
    using Validators;

    /// <summary>
    /// The <see cref="EditConnectionViewModel" /> class.
    /// </summary>
    public class EditConnectionViewModel : WPF.ViewModels.EditConnectionViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildProviderSettingsRepository">The build provider settings repository.</param>
        /// <param name="currentSettings">The current settings.</param>
        public EditConnectionViewModel(IEventAggregator eventAggregator, IBuildProviderSettingsRepository buildProviderSettingsRepository, BuildProviderSettings currentSettings)
            : base(eventAggregator, buildProviderSettingsRepository, currentSettings)
        {
            Validator = new EditConnectionViewModelValidator();
            Token = currentSettings.Settings.SingleOrDefault(setting => setting.Name == "Token")?.Value;
        }

        /// <summary>
        /// Gets the view name.
        /// </summary>
        /// <value>
        /// The view name.
        /// </value>
        public override string ViewName => "Edit AppVeyor connection";

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
        /// <param name="currentSettings">The current settings.</param>
        /// <returns>The settings.</returns>
        protected override BuildProviderSettings GetSettings(BuildProviderSettings currentSettings)
        {
            throw new NotImplementedException();
        }
    }
}
