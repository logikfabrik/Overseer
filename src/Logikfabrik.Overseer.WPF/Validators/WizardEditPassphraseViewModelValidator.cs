// <copyright file="WizardEditPassphraseViewModelValidator.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Validators
{
    using FluentValidation;
    using Properties;
    using ViewModels;

    /// <summary>
    /// The <see cref="WizardEditPassphraseViewModelValidator" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class WizardEditPassphraseViewModelValidator : AbstractValidator<WizardEditPassphraseViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WizardEditPassphraseViewModelValidator" /> class.
        /// </summary>
        public WizardEditPassphraseViewModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(viewModel => viewModel.Passphrase)
                .NotEmpty()
                .WithMessage(Resources.WizardEditPassphrase_Validation_Passphrase_NotEmpty)
                .MinimumLength(8)
                .WithMessage(Resources.WizardEditPassphrase_Validation_Passphrase_MinimumLength);
        }
    }
}
