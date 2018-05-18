// <copyright file="IEditConnectionSettingsViewModelFactory{T1,T2}.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using Settings;

    /// <summary>
    /// The <see cref="IEditConnectionSettingsViewModelFactory{T1,T2}" /> interface.
    /// </summary>
    /// <typeparam name="T1">The <see cref="ConnectionSettings" /> type.</typeparam>
    /// <typeparam name="T2">The <see cref="EditConnectionSettingsViewModel{T}" /> type.</typeparam>
    public interface IEditConnectionSettingsViewModelFactory<T1, out T2>
        where T1 : ConnectionSettings
        where T2 : EditConnectionSettingsViewModel<T1>, new()
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