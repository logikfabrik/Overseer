// <copyright file="INavigationMessageFactory{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Navigation.Factories
{
    /// <summary>
    /// The <see cref="INavigationMessageFactory{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The item type.</typeparam>
    public interface INavigationMessageFactory<T>
        where T : class
    {
        /// <summary>
        /// Creates a navigation message.
        /// </summary>
        /// <returns>A navigation message.</returns>
        NavigationMessage<T> Create();

        /// <summary>
        /// Creates a navigation message for the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A navigation message.</returns>
        NavigationMessage<T> Create(T item);
    }
}