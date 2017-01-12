// <copyright file="EditSettingsViewModelValidator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Validators
{
    using FluentValidation;
    using ViewModels;

    /// <summary>
    /// The <see cref="EditSettingsViewModelValidator" /> class. Validator for application wide settings.
    /// </summary>
    public class EditSettingsViewModelValidator : AbstractValidator<EditSettingsViewModel>
    {
    }
}
