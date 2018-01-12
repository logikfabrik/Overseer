// <copyright file="EditConnectionSettingsViewModelValidator{T1,T2}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Validators
{
    using FluentValidation;
    using Settings;
    using ViewModels;

    /// <summary>
    /// The <see cref="EditConnectionSettingsViewModelValidator{T1,T2}" /> class.
    /// </summary>
    /// <typeparam name="T1">The <see cref="EditConnectionSettingsViewModel{T}" /> type.</typeparam>
    /// <typeparam name="T2">The <see cref="ConnectionSettings" /> type.</typeparam>
    // ReSharper disable once InheritdocConsiderUsage
    public abstract class EditConnectionSettingsViewModelValidator<T1, T2> : AbstractValidator<T1>
        where T1 : EditConnectionSettingsViewModel<T2>
        where T2 : ConnectionSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionSettingsViewModelValidator{T1,T2}" /> class.
        /// </summary>
        protected EditConnectionSettingsViewModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(viewModel => viewModel.Name)
                .NotEmpty()
                .WithMessage(Properties.Resources.EditConnectionSettings_Validation_Name_NotEmpty);

            RuleFor(viewModel => viewModel.BuildsPerProject)
                .NotEmpty()
                .WithMessage(Properties.Resources.EditConnectionSettings_Validation_BuildsPerProject_NotEmpty)
                .InclusiveBetween(1, 10)
                .WithMessage(Properties.Resources.EditConnectionSettings_Validation_BuildsPerProject_InclusiveBetween);
        }
    }
}
