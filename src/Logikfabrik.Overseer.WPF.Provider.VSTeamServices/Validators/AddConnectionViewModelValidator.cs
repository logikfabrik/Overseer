// <copyright file="AddConnectionViewModelValidator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Validators
{
    using System;
    using FluentValidation;
    using ViewModels;

    /// <summary>
    /// The <see cref="AddConnectionViewModelValidator" /> class.
    /// </summary>
    public class AddConnectionViewModelValidator : AbstractValidator<AddConnectionViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionViewModelValidator" /> class.
        /// </summary>
        public AddConnectionViewModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(viewModel => viewModel.ConnectionName).NotEmpty();
            RuleFor(viewModel => viewModel.Url).NotEmpty().Must(url =>
            {
                Uri result;

                return Uri.TryCreate(url, UriKind.Absolute, out result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
            });
            RuleFor(viewModel => viewModel.Token).NotEmpty();
        }
    }
}
