// <copyright file="EditConnectionSettingsViewModelValidator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI.Validators
{
    using FluentValidation;
    using ViewModels;

    /// <summary>
    /// The <see cref="EditConnectionSettingsViewModelValidator" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class EditConnectionSettingsViewModelValidator : WPF.Validators.ConnectionSettingsViewModelValidator<EditConnectionSettingsViewModel, ConnectionSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionSettingsViewModelValidator" /> class.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public EditConnectionSettingsViewModelValidator()
        {
            RuleFor(viewModel => viewModel.Token)
                .NotEmpty()
                .WithMessage(viewModel => Properties.Resources.ConnectionSettings_Validation_Token);
        }
    }
}
