// <copyright file="IRemoveConnectionViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System;

    /// <summary>
    /// The <see cref="IRemoveConnectionViewModelFactory" /> interface.
    /// </summary>
    public interface IRemoveConnectionViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        RemoveConnectionViewModel CreateRemoveConnectionViewModel(Guid settingsId);
    }
}