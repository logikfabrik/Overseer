// <copyright file="IConnectionSettingsStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    /// <summary>
    /// The <see cref="IConnectionSettingsStore" /> interface.
    /// </summary>
    public interface IConnectionSettingsStore
    {
        /// <summary>
        /// Loads the settings.
        /// </summary>
        /// <returns>
        /// The settings.
        /// </returns>
        ConnectionSettings[] Load();

        /// <summary>
        /// Saves the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        void Save(ConnectionSettings[] settings);
    }
}