// <copyright file="ConnectionSettingsViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.ViewModels.Factories
{
    /// <summary>
    /// The <see cref="ConnectionSettingsViewModelFactory" /> class.
    /// </summary>
    public class ConnectionSettingsViewModelFactory : IConnectionSettingsViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <returns>
        /// A view model.
        /// </returns>
        public ConnectionSettingsViewModel Create()
        {
            return new ConnectionSettingsViewModel();
        }
    }
}
