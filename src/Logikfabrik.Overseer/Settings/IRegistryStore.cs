// <copyright file="IRegistryStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;

    /// <summary>
    /// The <see cref="IRegistryStore" /> interface.
    /// </summary>
    public interface IRegistryStore : IDisposable
    {
        /// <summary>
        /// Writes the specified key.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        void Write(string name, string value);

        /// <summary>
        /// Reads the specified key.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The value.</returns>
        string Read(string name);
    }
}