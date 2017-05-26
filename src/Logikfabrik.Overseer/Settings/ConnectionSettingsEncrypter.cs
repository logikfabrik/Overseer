// <copyright file="ConnectionSettingsEncrypter.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System.Xml;
    using EnsureThat;
    using Extensions;

    /// <summary>
    /// The <see cref="ConnectionSettingsEncrypter" /> class.
    /// </summary>
    public class ConnectionSettingsEncrypter : XmlEncrypter, IConnectionSettingsEncrypter
    {
        private static readonly byte[] Salt = { 155, 21, 6, 136, 63, 13, 179, 145, 46, 160, 245, 8, 208, 8, 50, 31 };

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsEncrypter" /> class.
        /// </summary>
        /// <param name="dataProtector">The data protector.</param>
        /// <param name="registryStore">The registry store.</param>
        public ConnectionSettingsEncrypter(IDataProtector dataProtector, IRegistryStore registryStore)
            : base(dataProtector, registryStore)
        {
        }

        /// <summary>
        /// Encrypts the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns>The encrypted XML.</returns>
        public string Encrypt(string xml)
        {
            this.ThrowIfDisposed(IsDisposed);

            Ensure.That(xml).IsNotNullOrWhiteSpace();

            var xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(xml);

            return Encrypt(xmlDocument, new[] { "Password", "Token" }).OuterXml;
        }

        /// <summary>
        /// Decrypts the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns>The decrypted XML.</returns>
        public string Decrypt(string xml)
        {
            this.ThrowIfDisposed(IsDisposed);

            Ensure.That(xml).IsNotNullOrWhiteSpace();

            var xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(xml);

            return Decrypt(xmlDocument, new[] { "EncryptedData" }).OuterXml;
        }

        /// <summary>
        /// Sets the pass phrase.
        /// </summary>
        /// <param name="passPhrase">The pass phrase.</param>
        public void SetPassPhrase(string passPhrase)
        {
            this.ThrowIfDisposed(IsDisposed);

            Ensure.That(passPhrase).IsNotNull();

            SetPassPhrase(passPhrase, Salt);
        }
    }
}
