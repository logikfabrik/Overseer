// <copyright file="IConnectionSettingsViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.ViewModels.Factories
{
    /// <summary>
    /// The <see cref="IConnectionSettingsViewModelFactory" /> interface.
    /// </summary>
    public interface IConnectionSettingsViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <returns>
        /// A view model.
        /// </returns>
        ConnectionSettingsViewModel Create();
    }
}