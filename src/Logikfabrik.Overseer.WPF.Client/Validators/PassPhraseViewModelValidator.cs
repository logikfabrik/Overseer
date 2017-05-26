// <copyright file="PassPhraseViewModelValidator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Validators
{
    using FluentValidation;
    using ViewModels;

    /// <summary>
    /// The <see cref="PassPhraseViewModelValidator" /> class.
    /// </summary>
    public class PassPhraseViewModelValidator : AbstractValidator<PassPhraseViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PassPhraseViewModelValidator" /> class.
        /// </summary>
        public PassPhraseViewModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            // ReSharper disable once ArgumentsStyleNamedExpression
            // ReSharper disable once ArgumentsStyleLiteral
            RuleFor(viewModel => viewModel.PassPhrase).NotEmpty().Length(min: 8, max: int.MaxValue);
        }
    }
}
