// <copyright file="NavigationMessage.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
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
        /// <param name="viewModelType">The view model type to navigate to.</param>
        public NavigationMessage(Type viewModelType)
            : this(viewModelType, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationMessage" /> class.
        /// </summary>
        /// <param name="viewModelType">The view model type to navigate to.</param>
        /// <param name="parameters">The parameters.</param>
        public NavigationMessage(Type viewModelType, params object[] parameters)
        {
            Ensure.That(viewModelType).IsNotNull();

            ViewModelType = viewModelType;
            Parameters = parameters;
        }

        /// <summary>
        /// Gets the type of the view model to navigate to.
        /// </summary>
        /// <value>
        /// The type of the view model to navigate to.
        /// </value>
        public Type ViewModelType { get; }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public IEnumerable<object> Parameters { get; }
    }
}
