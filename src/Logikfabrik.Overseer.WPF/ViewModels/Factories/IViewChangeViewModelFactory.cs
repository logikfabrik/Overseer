// <copyright file="IViewChangeViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    /// <summary>
    /// The <see cref="IViewChangeViewModelFactory" /> interface.
    /// </summary>
    public interface IViewChangeViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="change">The change.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        ViewChangeViewModel Create(IChange change);
    }
}
