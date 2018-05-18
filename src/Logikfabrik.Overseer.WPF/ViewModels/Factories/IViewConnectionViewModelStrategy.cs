// <copyright file="IViewConnectionViewModelStrategy.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using Settings;

    /// <summary>
    /// The <see cref="IViewConnectionViewModelStrategy" /> interface.
    /// </summary>
    public interface IViewConnectionViewModelStrategy
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        IViewConnectionViewModel Create(ConnectionSettings settings);
    }
}
