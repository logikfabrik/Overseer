// <copyright file="ConnectionSettingsViewModelValidator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Validators
{
    using System;
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
            RuleFor(viewModel => viewModel.Url)
                .NotEmpty()
                .Must(url =>
                {
                    Uri result;

                    return Uri.TryCreate(url, UriKind.Absolute, out result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
                })
                .WithMessage(viewModel => Properties.Resources.ConnectionSettings_Validation_Url);

            RuleFor(viewModel => viewModel.Username)
                .NotEmpty()
                .When(viewModel => viewModel.AuthenticationType == AuthenticationType.HttpAuth)
                .WithMessage(viewModel => Properties.Resources.ConnectionSettings_Validation_Username);

            RuleFor(viewModel => viewModel.Password)
                .NotEmpty()
                .When(viewModel => viewModel.AuthenticationType == AuthenticationType.HttpAuth)
                .WithMessage(viewModel => Properties.Resources.ConnectionSettings_Validation_Password);
        }
    }
}
