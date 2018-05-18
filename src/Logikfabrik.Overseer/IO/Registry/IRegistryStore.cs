// <copyright file="IRegistryStore.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.IO.Registry
{
    /// <summary>
    /// The <see cref="IRegistryStore" /> interface.
    /// </summary>
    public interface IRegistryStore
    {
        /// <summary>
        /// Writes the specified key.
        /// </summary>
        /// <param name="name">The key name.</param>
        /// <param name="value">The key value.</param>
        void Write(string name, string value);

        /// <summary>
        /// Reads the specified key.
        /// </summary>
        /// <param name="name">The key name.</param>
        /// <returns>The key value.</returns>
        string Read(string name);
    }
}