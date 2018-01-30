// <copyright file="INavigationMessage.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Navigation
{
    /// <summary>
    /// The <see cref="INavigationMessage" /> class.
    /// </summary>
    public interface INavigationMessage
    {
        /// <summary>
        /// Gets the item to navigate to.
        /// </summary>
        /// <value>
        /// The item to navigate to.
        /// </value>
        object Item { get; }
    }
}