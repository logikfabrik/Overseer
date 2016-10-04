// <copyright file="AddConnectionViewModelValidator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Validators
{
    using FluentValidation;
    using ViewModels;

    /// <summary>
    /// The <see cref="AddConnectionViewModelValidator" /> class.
    /// </summary>
    public class AddConnectionViewModelValidator : AbstractValidator<AddConnectionViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionViewModelValidator" /> class.
        /// </summary>
        public AddConnectionViewModelValidator()
        {
            RuleFor(viewModel => viewModel.ConnectionName).NotEmpty();
        }
    }
}
