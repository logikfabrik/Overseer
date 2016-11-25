// <copyright file="ConnectionSettingsViewModelValidator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Validators
{
    using System;
    using FluentValidation;
    using ViewModels;

    /// <summary>
    /// The <see cref="ConnectionSettingsViewModelValidator" /> class.
    /// </summary>
    public class ConnectionSettingsViewModelValidator : WPF.Validators.ConnectionSettingsViewModelValidator<ConnectionSettingsViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsViewModelValidator" /> class.
        /// </summary>
        public ConnectionSettingsViewModelValidator()
        {
            RuleFor(viewModel => viewModel.Url).NotEmpty().Must(url =>
            {
                Uri result;

                return Uri.TryCreate(url, UriKind.Absolute, out result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
            });
            RuleFor(viewModel => viewModel.Token).NotEmpty();
        }
    }
}
