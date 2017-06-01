// <copyright file="NavigationMessage.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Navigation
{
    using System;
    using System.Collections.Generic;
    using EnsureThat;

    /// <summary>
    /// The <see cref="NavigationMessage" /> class.
    /// </summary>
    public class NavigationMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationMessage" /> class.
        /// </summary>
        /// <param name="to">The target to navigate to.</param>
        /// <param name="from">The targets to navigate from.</param>
        public NavigationMessage(NavigationTarget to, IEnumerable<NavigationTarget> from)
        {
            Ensure.That(to).IsNotNull();
            Ensure.That(from).IsNotNull();

            To = to;
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationMessage" /> class.
        /// </summary>
        /// <param name="to">The target to navigate to.</param>
        /// <param name="from">The target to navigate from.</param>
        public NavigationMessage(NavigationTarget to, NavigationTarget from)
        {
            Ensure.That(to).IsNotNull();
            Ensure.That(from).IsNotNull();

            To = to;
            From = new[] { from };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationMessage" /> class.
        /// </summary>
        /// <param name="to">The target to navigate to.</param>
        public NavigationMessage(NavigationTarget to)
            : this(to, new NavigationTarget[] { })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationMessage" /> class.
        /// </summary>
        /// <param name="item">The item to navigate to.</param>
        public NavigationMessage(object item)
            : this(new NavigationTarget(item))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationMessage"/> class.
        /// </summary>
        /// <param name="itemType">The item type to navigate to.</param>
        public NavigationMessage(Type itemType)
            : this(new NavigationTarget(itemType))
        {
        }

        /// <summary>
        /// Gets the target to navigate to.
        /// </summary>
        /// <value>
        /// The target to navigate to.
        /// </value>
        public NavigationTarget To { get; }

        /// <summary>
        /// Gets the targets to navigate from.
        /// </summary>
        /// <value>
        /// The targets to navigate from.
        /// </value>
        public IEnumerable<NavigationTarget> From { get; }
    }
}