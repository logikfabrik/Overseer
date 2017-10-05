// <copyright file="IFileSystem.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    /// <summary>
    /// The <see cref="IFileSystem" /> interface.
    /// </summary>
    public interface IFileSystem
    {
        bool FileExists(string path);

        string ReadFileText(string path);

        void WriteFileText(string path, string text);

        void CreateDirectory(string path);
    }
}
