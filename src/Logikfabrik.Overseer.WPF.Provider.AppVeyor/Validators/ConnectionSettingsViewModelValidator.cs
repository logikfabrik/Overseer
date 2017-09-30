// <copyright file="ConnectionSettingsViewModelValidator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.Validators
{
    using FluentValidation;
    using ViewModels;

    /// <summary>
    /// The <see cref="ConnectionSettingsViewModelValidator" /> class.
    /// </summary>
    public class ConnectionSettingsViewModelValidator : WPF.Validators.ConnectionSettingsViewModelValidator<ConnectionSettingsViewModel, ConnectionSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsViewModelValidator" /> class.
        /// </summary>
        public ConnectionSettingsViewModelValidator()
        {
            RuleFor(viewModel => viewModel.Token)
                .NotEmpty();
        }
    }
}
