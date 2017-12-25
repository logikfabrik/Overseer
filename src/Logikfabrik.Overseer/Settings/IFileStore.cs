// <copyright file="IFileStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    /// <summary>
    /// The <see cref="IFileStore" /> interface.
    /// </summary>
    public interface IFileStore
    {
        /// <summary>
        /// Reads the file text.
        /// </summary>
        /// <returns>
        /// The file text.
        /// </returns>
        string Read();

        /// <summary>
        /// Writes the specified file text to the file.
        /// </summary>
        /// <param name="text">The file text to write.</param>
        void Write(string text);
    }
}