// <copyright file="IConnectionViewModelFactory{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using Settings;

    // TODO: Remove this interface?

    /// <summary>
    /// The <see cref="IConnectionViewModelFactory{T}" /> interface.
    /// </summary>
    /// <typeparam name="T">The <see cref="ConnectionViewModel" /> type.</typeparam>
    public interface IConnectionViewModelFactory<out T> : IConnectionViewModelFactory
        where T : ConnectionViewModel
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        new T Create(ConnectionSettings settings);
    }
}