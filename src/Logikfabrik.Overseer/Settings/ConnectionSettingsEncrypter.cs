// <copyright file="ConnectionSettingsEncrypter.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System.Xml;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionSettingsEncrypter" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ConnectionSettingsEncrypter : XmlEncrypter, IConnectionSettingsEncrypter
    {
        private static readonly byte[] Salt = { 155, 21, 6, 136, 63, 13, 179, 145, 46, 160, 245, 8, 208, 8, 50, 31 };

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsEncrypter" /> class.
        /// </summary>
        /// <param name="dataProtector">The data protector.</param>
        /// <param name="registryStore">The registry store.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public ConnectionSettingsEncrypter(IDataProtector dataProtector, IRegistryStore registryStore)
            : base(dataProtector, registryStore)
        {
        }

        /// <inheritdoc />
        public string Encrypt(string xml)
        {
            Ensure.That(xml).IsNotNullOrWhiteSpace();

            var xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(xml);

            return Encrypt(xmlDocument, new[] { "Password", "Token" }).OuterXml;
        }

        /// <inheritdoc />
        public string Decrypt(string xml)
        {
            Ensure.That(xml).IsNotNullOrWhiteSpace();

            var xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(xml);

            return Decrypt(xmlDocument, new[] { "EncryptedData" }).OuterXml;
        }

        /// <inheritdoc />
        public void SetPassphrase(string passphrase)
        {
            Ensure.That(passphrase).IsNotNull();

            SetPassphrase(passphrase, Salt);
        }
    }
}
