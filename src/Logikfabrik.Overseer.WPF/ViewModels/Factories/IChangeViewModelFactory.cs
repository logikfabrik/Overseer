// <copyright file="IChangeViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    /// <summary>
    /// The <see cref="IChangeViewModelFactory" /> interface.
    /// </summary>
    public interface IChangeViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="change">The change.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        ChangeViewModel Create(IChange change);
    }
}
