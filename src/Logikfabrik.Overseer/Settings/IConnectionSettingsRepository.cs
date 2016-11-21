// <copyright file="IConnectionSettingsRepository.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="IConnectionSettingsRepository" /> interface.
    /// </summary>
    public interface IConnectionSettingsRepository
    {
        /// <summary>
        /// Adds the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        void Add(ConnectionSettings settings);

        /// <summary>
        /// Removes the settings with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Remove(Guid id);

        /// <summary>
        /// Updates the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        void Update(ConnectionSettings settings);

        /// <summary>
        /// Gets all the settings.
        /// </summary>
        /// <returns>
        /// All the settings.
        /// </returns>
        IEnumerable<ConnectionSettings> GetAll();
    }
}