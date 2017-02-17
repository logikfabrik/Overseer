// <copyright file="IConnectionSettingsViewModelFactory{T1,T2}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using Settings;

    /// <summary>
    /// The <see cref="IConnectionSettingsViewModelFactory{T1,T2}" /> interface.
    /// </summary>
    /// <typeparam name="T1">The <see cref="ConnectionSettings" /> type.</typeparam>
    /// <typeparam name="T2">The <see cref="ConnectionSettingsViewModel{T}" /> type.</typeparam>
    public interface IConnectionSettingsViewModelFactory<T1, out T2>
        where T1 : ConnectionSettings
        where T2 : ConnectionSettingsViewModel<T1>, new()
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <returns>
        /// A view model.
        /// </returns>
        T2 Create();
    }
}