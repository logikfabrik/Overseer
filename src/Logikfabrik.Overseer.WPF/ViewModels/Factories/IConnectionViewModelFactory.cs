// <copyright file="IConnectionViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System;
    using Settings;

    /// <summary>
    /// The <see cref="IConnectionViewModelFactory" /> interface.
    /// </summary>
    public interface IConnectionViewModelFactory
    {
        /// <summary>
        /// Gets the type this factory applies to.
        /// </summary>
        /// <value>
        /// The type this factory applies to.
        /// </value>
        Type AppliesTo { get; }

        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        IConnectionViewModel Create(ConnectionSettings settings);
    }
}
