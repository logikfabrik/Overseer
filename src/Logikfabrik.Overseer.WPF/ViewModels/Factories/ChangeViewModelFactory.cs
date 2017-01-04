// <copyright file="ChangeViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    /// <summary>
    /// The <see cref="ChangeViewModelFactory" /> class.
    /// </summary>
    public class ChangeViewModelFactory : IChangeViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="change">The change.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        public ChangeViewModel Create(IChange change)
        {
            return new ChangeViewModel(change);
        }
    }
}
