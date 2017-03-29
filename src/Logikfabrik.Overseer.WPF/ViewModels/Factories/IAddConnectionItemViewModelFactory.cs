// <copyright file="IAddConnectionItemViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    /// <summary>
    /// The <see cref="IAddConnectionItemViewModelFactory" /> interface.
    /// </summary>
    public interface IAddConnectionItemViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <returns>A view model.</returns>
        AddConnectionItemViewModel Create();
    }
}
