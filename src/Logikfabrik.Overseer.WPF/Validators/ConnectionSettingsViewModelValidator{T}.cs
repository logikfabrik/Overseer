// <copyright file="ConnectionSettingsViewModelValidator{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Validators
{
    using FluentValidation;
    using ViewModels;

    /// <summary>
    /// The <see cref="ConnectionSettingsViewModelValidator{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The <see cref="ConnectionSettingsViewModel" /> type.</typeparam>
    /// <seealso cref="FluentValidation.AbstractValidator{T}" />
    public abstract class ConnectionSettingsViewModelValidator<T> : AbstractValidator<T>
        where T : ConnectionSettingsViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsViewModelValidator{T}" /> class.
        /// </summary>
        protected ConnectionSettingsViewModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(viewModel => viewModel.Name).NotEmpty();
        }
    }
}
