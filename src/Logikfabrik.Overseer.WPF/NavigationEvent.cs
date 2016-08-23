// <copyright file="NavigationEvent.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;

    /// <summary>
    /// The <see cref="NavigationEvent" /> class.
    /// </summary>
    public class NavigationEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationEvent" /> class.
        /// </summary>
        /// <param name="viewModelType">The view model type to navigate to.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="viewModelType" /> is <c>null</c>.</exception>
        public NavigationEvent(Type viewModelType)
        {
            if (viewModelType == null)
            {
                throw new ArgumentNullException(nameof(viewModelType));
            }

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
