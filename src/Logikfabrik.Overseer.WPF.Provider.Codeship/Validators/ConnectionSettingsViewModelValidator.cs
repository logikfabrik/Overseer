// <copyright file="ConnectionSettingsViewModelValidator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.Codeship.Validators
{
    using FluentValidation;
    using ViewModels;

    /// <summary>
    /// The <see cref="ConnectionSettingsViewModelValidator" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ConnectionSettingsViewModelValidator : WPF.Validators.ConnectionSettingsViewModelValidator<ConnectionSettingsViewModel, ConnectionSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsViewModelValidator" /> class.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public ConnectionSettingsViewModelValidator()
        {
            RuleFor(viewModel => viewModel.Username)
                .NotEmpty()
                .WithMessage(viewModel => Properties.Resources.ConnectionSettings_Validation_Username);

            RuleFor(viewModel => viewModel.Password)
                .NotEmpty()
                .WithMessage(viewModel => Properties.Resources.ConnectionSettings_Validation_Password);
        }
    }
}
