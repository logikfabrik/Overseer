// <copyright file="FileSystem.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System.IO;

    // TODO: Move up one namespace.

    /// <summary>
    /// The <see cref="FileSystem" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class FileSystem : IFileSystem
    {
        /// <inheritdoc />
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        /// <inheritdoc />
        public string ReadFileText(string path)
        {
            return File.ReadAllText(path);
        }

        /// <inheritdoc />
        public void WriteFileText(string path, string text)
        {
            File.WriteAllText(path, text);
        }

        /// <inheritdoc />
        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}
