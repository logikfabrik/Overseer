// <copyright file="ConnectionSettingsEncrypter.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System.Xml;
    using EnsureThat;
    using JetBrains.Annotations;
    using Passphrase;
    using Security.Xml;

    /// <summary>
    /// The <see cref="ConnectionSettingsEncrypter" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ConnectionSettingsEncrypter : XmlEncrypter, IConnectionSettingsEncrypter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsEncrypter" /> class.
        /// </summary>
        /// <param name="passphraseRepository">The passphrase repository.</param>
        [UsedImplicitly]

        // ReSharper disable once InheritdocConsiderUsage
        public ConnectionSettingsEncrypter(IPassphraseRepository passphraseRepository)
            : base(passphraseRepository)
        {
        }

        /// <inheritdoc />
        public string Encrypt(string xml)
        {
            Ensure.That(xml).IsNotNullOrWhiteSpace();

            var xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(xml);

            var tagsToEncrypt = new[] { "Password", "Token" };

            return Encrypt(xmlDocument, tagsToEncrypt).OuterXml;
        }

        /// <inheritdoc />
        public string Decrypt(string xml)
        {
            Ensure.That(xml).IsNotNullOrWhiteSpace();

            var xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(xml);

            var tagsToDecrypt = new[] { "EncryptedData" };

            return Decrypt(xmlDocument, tagsToDecrypt).OuterXml;
        }
    }
}
