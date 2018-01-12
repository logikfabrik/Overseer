// <copyright file="EditSettingsViewModelValidator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Validators
{
    using System.Linq;
    using FluentValidation;
    using Localization;
    using ViewModels;

    /// <summary>
    /// The <see cref="EditSettingsViewModelValidator" /> class. Validator for application wide settings.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class EditSettingsViewModelValidator : AbstractValidator<EditSettingsViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditSettingsViewModelValidator" /> class.
        /// </summary>
        public EditSettingsViewModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(viewModel => viewModel.Interval)
                .NotEmpty()
                .WithMessage(Properties.Resources.EditSettings_Validation_Interval_NotEmpty)
                .InclusiveBetween(10, 86400)
                .WithMessage(Properties.Resources.EditSettings_Validation_Interval_InclusiveBetween);

            RuleFor(viewModel => viewModel.CultureName)
                .NotEmpty()
                .Must(cultureName => SupportedCultures.CultureNames.Contains(cultureName))
                .WithMessage(Properties.Resources.EditSettings_Validation_CultureName);
        }
    }
}
