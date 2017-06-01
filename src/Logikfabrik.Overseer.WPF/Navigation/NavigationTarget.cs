// <copyright file="NavigationTarget.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Navigation
{
    using System;
    using EnsureThat;

    /// <summary>
    /// The <see cref="NavigationTarget" /> class.
    /// </summary>
    public class NavigationTarget
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationTarget" /> class.
        /// </summary>
        /// <param name="itemType">The item type to navigate to.</param>
        public NavigationTarget(Type itemType)
        {
            Ensure.That(itemType).IsNotNull();

            ItemType = itemType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationTarget" /> class.
        /// </summary>
        /// <param name="item">The item to navigate to.</param>
        public NavigationTarget(object item)
            : this(item.GetType())
        {
            Ensure.That(item).IsNotNull();

            Item = item;
        }

        /// <summary>
        /// Gets the item type to navigate to.
        /// </summary>
        /// <value>
        /// The item type to navigate to.
        /// </value>
        public Type ItemType { get; }

        /// <summary>
        /// Gets the item to navigate to.
        /// </summary>
        /// <value>
        /// The item to navigate to.
        /// </value>
        public object Item { get; }
    }
}