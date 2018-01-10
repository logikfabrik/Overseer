// <copyright file="PassPhraseWizardStepViewModelValidator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

using Logikfabrik.Overseer.WPF.Client.Properties;
using Logikfabrik.Overseer.WPF.Client.ViewModels.Wizard;

namespace Logikfabrik.Overseer.WPF.Client.Validators
{
    using FluentValidation;
    using ViewModels;

    /// <summary>
    /// The <see cref="PassPhraseWizardStepViewModelValidator" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class PassPhraseWizardStepViewModelValidator : AbstractValidator<PassPhraseWizardStepViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PassPhraseWizardStepViewModelValidator" /> class.
        /// </summary>
        public PassPhraseWizardStepViewModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(viewModel => viewModel.PassPhrase)
                .NotEmpty().WithLocalizedMessage(
                    typeof(Resources),
                    nameof(Resources.PassphraseWizardStep_Validation_Passphrase_NotEmpty))
                .MinimumLength(8).WithLocalizedMessage(
                    typeof(Resources),
                    nameof(Resources.PassphraseWizardStep_Validation_Passphrase_MinimumLength));
        }
    }
}
