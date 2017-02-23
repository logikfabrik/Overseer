// <copyright file="IEditConnectionViewModelFactory{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using Settings;

    /// <summary>
    /// The <see cref="IEditConnectionViewModelFactory{T}" /> interface.
    /// </summary>
    /// <typeparam name="T">The <see cref="ConnectionSettings" /> type.</typeparam>
    public interface IEditConnectionViewModelFactory<T>
        where T : ConnectionSettings
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="currentSettings">The current settings.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        EditConnectionViewModel<T> Create(ConnectionSettings currentSettings);
    }
}