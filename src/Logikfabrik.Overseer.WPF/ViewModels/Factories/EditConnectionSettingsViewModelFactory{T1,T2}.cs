// <copyright file="EditConnectionSettingsViewModelFactory{T1,T2}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using Settings;

    /// <summary>
    /// The <see cref="EditConnectionSettingsViewModelFactory{T1,T2}" /> class.
    /// </summary>
    /// <typeparam name="T1">The <see cref="ConnectionSettings" /> type.</typeparam>
    /// <typeparam name="T2">The <see cref="EditConnectionSettingsViewModel{T}" /> type.</typeparam>
    // ReSharper disable once InheritdocConsiderUsage
    public class EditConnectionSettingsViewModelFactory<T1, T2> : IEditConnectionSettingsViewModelFactory<T1, T2>
        where T1 : ConnectionSettings
        where T2 : EditConnectionSettingsViewModel<T1>, new()
    {
        /// <inheritdoc />
        public T2 Create()
        {
            return new T2();
        }
    }
}
