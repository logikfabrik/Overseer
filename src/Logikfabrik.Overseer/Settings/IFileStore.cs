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
        /// Reads the file.
        /// </summary>
        /// <returns>
        /// The contents.
        /// </returns>
        string Read();

        /// <summary>
        /// Writes the specified contents to the file.
        /// </summary>
        /// <param name="contents">The contents.</param>
        void Write(string contents);
    }
}