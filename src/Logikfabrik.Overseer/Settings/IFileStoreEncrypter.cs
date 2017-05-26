// <copyright file="IFileStoreEncrypter.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    /// <summary>
    /// The <see cref="IFileStoreEncrypter" /> interface.
    /// </summary>
    public interface IFileStoreEncrypter
    {
        /// <summary>
        /// Encrypts the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns>The encrypted XML.</returns>
        string Encrypt(string xml);

        /// <summary>
        /// Decrypts the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns>The decrypted XML.</returns>
        string Decrypt(string xml);
    }
}
