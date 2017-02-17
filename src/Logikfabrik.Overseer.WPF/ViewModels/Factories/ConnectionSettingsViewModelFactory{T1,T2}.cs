// <copyright file="ConnectionSettingsViewModelFactory{T1,T2}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using Overseer.Settings;

    /// <summary>
    /// The <see cref="ConnectionSettingsViewModelFactory{T1,T2}" /> class.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class ConnectionSettingsViewModelFactory<T1, T2> : IConnectionSettingsViewModelFactory<T1, T2>
        where T1 : ConnectionSettings
        where T2 : ConnectionSettingsViewModel<T1>, new()
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <returns>
        /// A view model.
        /// </returns>
        public T2 Create()
        {
            return new T2();
        }
    }
}
