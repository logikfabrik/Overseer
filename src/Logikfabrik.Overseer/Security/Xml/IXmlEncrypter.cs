// <copyright file="IXmlEncrypter.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Security.Xml
{
    using System.Xml;

    /// <summary>
    /// The <see cref="IXmlEncrypter" /> interface.
    /// </summary>
    public interface IXmlEncrypter
    {
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
    }
}
