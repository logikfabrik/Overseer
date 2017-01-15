// <copyright file="EditSettingsViewModelValidator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Validators
{
    using System;
    using FluentValidation;
    using ViewModels;

    /// <summary>
    /// The <see cref="EditSettingsViewModelValidator" /> class. Validator for application wide settings.
    /// </summary>
    public class EditSettingsViewModelValidator : AbstractValidator<EditSettingsViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditSettingsViewModelValidator" /> class.
        /// </summary>
        public EditSettingsViewModelValidator()
        {
            RuleFor(viewModel => viewModel.ProxyUrl).Must(url =>
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    return true;
                }

                Uri result;

                return Uri.TryCreate(url, UriKind.Absolute, out result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
            });
        }
    }
}
