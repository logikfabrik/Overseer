// <copyright file="NavigationMessage{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Navigation
{
    using EnsureThat;

    /// <summary>
    /// The <see cref="NavigationMessage{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The item type.</typeparam>
    // ReSharper disable once InheritdocConsiderUsage
    public class NavigationMessage<T> : INavigationMessage
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationMessage{T}" /> class.
        /// </summary>
        /// <param name="item">The item to navigate to.</param>
        public NavigationMessage(T item)
        {
            Ensure.That(item).IsNotNull();

            Item = item;
        }

        /// <summary>
        /// Gets the item to navigate to.
        /// </summary>
        /// <value>
        /// The item to navigate to.
        /// </value>
        public T Item { get; }

        /// <inheritdoc/>
        object INavigationMessage.Item => Item;
    }
}