// <copyright file="ConnectionSettingsViewModelValidator{T1,T2}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Validators
{
    using FluentValidation;
    using Settings;
    using ViewModels;

    /// <summary>
    /// The <see cref="ConnectionSettingsViewModelValidator{T1,T2}" /> class.
    /// </summary>
    /// <typeparam name="T1">The <see cref="EditConnectionSettingsViewModel{T}" /> type.</typeparam>
    /// <typeparam name="T2">The <see cref="ConnectionSettings" /> type.</typeparam>
    // ReSharper disable once InheritdocConsiderUsage
    public abstract class ConnectionSettingsViewModelValidator<T1, T2> : AbstractValidator<T1>
        where T1 : EditConnectionSettingsViewModel<T2>
        where T2 : ConnectionSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsViewModelValidator{T1,T2}" /> class.
        /// </summary>
        protected ConnectionSettingsViewModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(viewModel => viewModel.Name)
                .NotEmpty()
                .WithMessage(viewModel => Properties.Resources.ConnectionSettings_Validation_Name);

            RuleFor(viewModel => viewModel.BuildsPerProject)
                .GreaterThan(0)
                .WithMessage(viewModel => Properties.Resources.ConnectionSettings_Validation_BuildsPerProject);
        }
    }
}
