// <copyright file="FileSystem.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System.IO;

    /// <summary>
    /// The <see cref="FileSystem" /> class.
    /// </summary>
    public class FileSystem : IFileSystem
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public string ReadFileText(string path)
        {
            return File.ReadAllText(path);
        }

        public void WriteFileText(string path, string text)
        {
            File.WriteAllText(path, text);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}
