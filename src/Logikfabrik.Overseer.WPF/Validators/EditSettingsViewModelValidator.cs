// <copyright file="EditSettingsViewModelValidator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Validators
{
    using System.Globalization;
    using System.Linq;
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
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(viewModel => viewModel.Interval)
                .InclusiveBetween(10, 86400)
                .WithMessage(viewModel => Properties.Resources.EditSettings_Validation_Interval);

            RuleFor(viewModel => viewModel.CultureName)
                .NotEmpty()
                .Must(cultureName =>
                {
                    return CultureInfo
                        .GetCultures(CultureTypes.SpecificCultures)
                        .Any(c => c.Name == cultureName);
                })
                .WithMessage(viewModel => Properties.Resources.EditSettings_Validation_CultureName);
        }
    }
}
