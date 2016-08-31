// <copyright file="FileAttributesExtensions.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System.IO;

    /// <summary>
    /// The <see cref="FileAttributesExtensions" /> class.
    /// </summary>
    public static class FileAttributesExtensions
    {
        public static bool IsEncrypted(this FileAttributes fileAttributes)
        {
            return fileAttributes.HasFlag(FileAttributes.Encrypted);
        }
    }
}
