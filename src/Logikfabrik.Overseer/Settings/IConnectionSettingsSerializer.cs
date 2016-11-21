// <copyright file="IConnectionSettingsSerializer.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    /// <summary>
    /// The <see cref="IConnectionSettingsSerializer" /> interface.
    /// </summary>
    public interface IConnectionSettingsSerializer
    {
        /// <summary>
        /// Deserializes the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// The deserialized settings.
        /// </returns>
        ConnectionSettings[] Deserialize(string settings);

        /// <summary>
        /// Serializes the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// The serialized settings.
        /// </returns>
        string Serialize(ConnectionSettings[] settings);
    }
}