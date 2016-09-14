// <copyright file="NavigationEvent.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using EnsureThat;

    /// <summary>
    /// The <see cref="NavigationEvent" /> class.
    /// </summary>
    public class NavigationEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationEvent" /> class.
        /// </summary>
        /// <param name="viewModelType">The view model type to navigate to.</param>
        public NavigationEvent(Type viewModelType)
        {
            Ensure.That(viewModelType).IsNotNull();

            ViewModelType = viewModelType;
        }

        /// <summary>
        /// Gets the type of the view model.
        /// </summary>
        /// <value>
        /// The type of the view model.
        /// </value>
        public Type ViewModelType { get; }
    }
}
