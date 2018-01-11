// <copyright file="PassphraseWizardStepViewModelValidator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

using Logikfabrik.Overseer.WPF.Client.ViewModels;

namespace Logikfabrik.Overseer.WPF.Client.Validators
{
    using FluentValidation;
    using Properties;

    /// <summary>
    /// The <see cref="PassphraseWizardStepViewModelValidator" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class PassphraseWizardStepViewModelValidator : AbstractValidator<PassphraseWizardStepViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PassphraseWizardStepViewModelValidator" /> class.
        /// </summary>
        public PassphraseWizardStepViewModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(viewModel => viewModel.Passphrase)
                .NotEmpty().WithLocalizedMessage(
                    typeof(Resources),
                    nameof(Resources.PassphraseWizardStep_Validation_Passphrase_NotEmpty))
                .MinimumLength(8).WithLocalizedMessage(
                    typeof(Resources),
                    nameof(Resources.PassphraseWizardStep_Validation_Passphrase_MinimumLength));
        }
    }
}
