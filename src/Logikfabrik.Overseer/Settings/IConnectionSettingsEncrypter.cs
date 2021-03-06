﻿// <copyright file="IConnectionSettingsEncrypter.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using Security.Xml;

    /// <summary>
    /// The <see cref="IConnectionSettingsEncrypter" /> interface.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public interface IConnectionSettingsEncrypter : IXmlEncrypter
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
