// <copyright file="NavigationMessage2.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using EnsureThat;
    using ViewModels;

    /// <summary>
    /// The <see cref="NavigationMessage2" /> class.
    /// </summary>
    public class NavigationMessage2 : NavigationMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationMessage2" /> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public NavigationMessage2(IViewModel viewModel)
            : base(viewModel.GetType())
        {
            Ensure.That(viewModel).IsNotNull();

            ViewModel = viewModel;
        }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        public IViewModel ViewModel { get; }
    }
}
