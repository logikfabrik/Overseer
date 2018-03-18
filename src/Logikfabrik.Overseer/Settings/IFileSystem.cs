// <copyright file="IFileSystem.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    // TODO: Move up one namespace.

    /// <summary>
    /// The <see cref="IFileSystem" /> interface.
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// Determines whether the specified file exists.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <returns><c>true</c> if the specified file exists; otherwise, <c>false</c>.</returns>
        bool FileExists(string path);

        /// <summary>
        /// Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <returns>A string containing all lines of the file.</returns>
        string ReadFileText(string path);

        /// <summary>
        /// Creates a new file, writes the specified text to the file, and then closes the file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="text">The text.</param>
        void WriteFileText(string path, string text);

        /// <summary>
        /// Creates all directories and subdirectories in the specified path unless they already exist.
        /// </summary>
        /// <param name="path">The directory path.</param>
        void CreateDirectory(string path);
    }
}
