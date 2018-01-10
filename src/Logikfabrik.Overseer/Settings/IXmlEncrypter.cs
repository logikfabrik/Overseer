﻿// <copyright file="IXmlEncrypter.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System.Xml;

    /// <summary>
    /// The <see cref="IXmlEncrypter" /> interface.
    /// </summary>
    public interface IXmlEncrypter
    {
        /// <summary>
        /// Gets a value indicating whether this instance has a passphrase.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has a passphrase; otherwise, <c>false</c>.
        /// </value>
        bool HasPassphrase { get; }

        /// <summary>
        /// Encrypts the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <param name="tagNames">The tag names of elements to encrypt.</param>
        /// <returns>The encrypted XML.</returns>
        XmlDocument Encrypt(XmlDocument xml, string[] tagNames);

        /// <summary>
        /// Decrypts the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <param name="tagNames">The tag names of elements to decrypt.</param>
        /// <returns>The decrypted XML.</returns>
        XmlDocument Decrypt(XmlDocument xml, string[] tagNames);

        /// <summary>
        /// Sets the passphrase.
        /// </summary>
        /// <param name="passphrase">The passphrase.</param>
        /// <param name="salt">The salt.</param>
        void SetPassphrase(string passphrase, byte[] salt);
    }
}
