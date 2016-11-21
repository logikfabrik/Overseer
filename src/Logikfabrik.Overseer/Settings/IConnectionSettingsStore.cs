// <copyright file="IConnectionSettingsStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System.Threading.Tasks;

    /// <summary>
    /// The <see cref="IConnectionSettingsStore" /> interface.
    /// </summary>
    public interface IConnectionSettingsStore
    {
        /// <summary>
        /// Loads the settings.
        /// </summary>
        /// <returns>
        /// A task.
        /// </returns>
        Task<ConnectionSettings[]> LoadAsync();

        /// <summary>
        /// Saves the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// A task.
        /// </returns>
        Task SaveAsync(ConnectionSettings[] settings);
    }
}