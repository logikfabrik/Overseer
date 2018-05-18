// <copyright file="XmlEncrypter.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Security.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Security.Cryptography.Xml;
    using System.Xml;
    using EnsureThat;
    using Passphrase;

    /// <summary>
    /// The <see cref="XmlEncrypter" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class XmlEncrypter : IXmlEncrypter
    {
        private readonly IPassphraseRepository _passphraseRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlEncrypter" /> class.
        /// </summary>
        /// <param name="passphraseRepository">The passphrase repository.</param>
        public XmlEncrypter(IPassphraseRepository passphraseRepository)
        {
            Ensure.That(passphraseRepository).IsNotNull();

            _passphraseRepository = passphraseRepository;
        }

        /// <inheritdoc />
        public XmlDocument Encrypt(XmlDocument xml, string[] tagNames)
        {
            Ensure.That(xml).IsNotNull();
            Ensure.That(tagNames).IsNotNull();

            using (var algorithm = GetAlgorithm())
            {
                return Encrypt(xml, tagNames, algorithm);
            }
        }

        /// <inheritdoc />
        public XmlDocument Decrypt(XmlDocument xml, string[] tagNames)
        {
            Ensure.That(xml).IsNotNull();
            Ensure.That(tagNames).IsNotNull();

            using (var algorithm = GetAlgorithm())
            {
                return Decrypt(xml, tagNames, algorithm);
            }
        }

        // ReSharper disable once SuggestBaseTypeForParameter
#pragma warning disable S3242 // Method parameters should be declared with base types
        private static XmlDocument Encrypt(XmlDocument xml, IEnumerable<string> tagNames, Rijndael algorithm)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            foreach (var tagName in tagNames)
            {
                var elements = xml.GetElementsByTagName(tagName);

                while (elements.Count > 0)
                {
                    var element = (XmlElement)elements[0];

                    var encryptedData = new EncryptedData
                    {
                        CipherData =
                        {
                            CipherValue = new EncryptedXml().EncryptData(element, algorithm, false)
                        }
                    };

                    switch (algorithm.KeySize)
                    {
                        case 128:
                            encryptedData.EncryptionMethod = new EncryptionMethod(EncryptedXml.XmlEncAES128Url);
                            break;

                        default:
                            throw new NotSupportedException();
                    }

                    EncryptedXml.ReplaceElement(element, encryptedData, false);

                    elements = xml.GetElementsByTagName(tagName);
                }
            }

            return xml;
        }

        private static XmlDocument Decrypt(XmlDocument xml, IEnumerable<string> tagNames, SymmetricAlgorithm algorithm)
        {
            foreach (var tagName in tagNames)
            {
                var elements = xml.GetElementsByTagName(tagName);

                while (elements.Count > 0)
                {
                    var element = (XmlElement)elements[0];

                    DecryptElement(element, algorithm);

                    elements = xml.GetElementsByTagName(nameof(EncryptedData));
                }
            }

            return xml;
        }

        private static Rijndael GetAlgorithm(byte[] passphraseHash)
        {
            var key = new byte[16];

            Array.Copy(passphraseHash, 0, key, 0, 16);

            var algorithm = Rijndael.Create();

            algorithm.KeySize = 128;
            algorithm.BlockSize = 128;
            algorithm.Key = key;
            algorithm.IV = HashUtility.GetSalt(16);

            return algorithm;
        }

        private static void DecryptElement(XmlElement element, SymmetricAlgorithm algorithm)
        {
            var encryptedData = new EncryptedData();

            encryptedData.LoadXml(element);

            var encryptedXml = new EncryptedXml();

            var decryptedData = encryptedXml.DecryptData(encryptedData, algorithm);

            encryptedXml.ReplaceData(element, decryptedData);
        }

        private Rijndael GetAlgorithm()
        {
            return GetAlgorithm(_passphraseRepository.ReadHash());
        }
    }
}