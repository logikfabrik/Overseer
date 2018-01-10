// <copyright file="XmlEncrypter.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Security.Cryptography.Xml;
    using System.Xml;
    using EnsureThat;

    /// <summary>
    /// The <see cref="XmlEncrypter" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class XmlEncrypter : IXmlEncrypter
    {
        /// <summary>
        /// The key name.
        /// </summary>
        public const string KeyName = "PassPhrase";

        private readonly IDataProtector _dataProtector;
        private readonly IRegistryStore _registryStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlEncrypter" /> class.
        /// </summary>
        /// <param name="dataProtector">The data protector.</param>
        /// <param name="registryStore">The registry store.</param>
        public XmlEncrypter(IDataProtector dataProtector, IRegistryStore registryStore)
        {
            Ensure.That(dataProtector).IsNotNull();
            Ensure.That(registryStore).IsNotNull();

            _dataProtector = dataProtector;
            _registryStore = registryStore;

            HasPassphrase = ReadPassphraseHash().Any();
        }

        /// <inheritdoc />
        public bool HasPassphrase { get; private set; }

        /// <inheritdoc />
        public void SetPassphrase(string passphrase, byte[] salt)
        {
            Ensure.That(passphrase).IsNotNullOrWhiteSpace();
            Ensure.That(salt).IsNotNull();
            Ensure.That(salt.Length % 16).Is(0);

            var passphraseHash = HashUtility.GetHash(passphrase, salt, 32);

            WritePassphraseHash(passphraseHash);

            HasPassphrase = true;
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

        /// <summary>
        /// Writes the passphrase hash.
        /// </summary>
        /// <param name="passphraseHash">The passphrase hash.</param>
        internal void WritePassphraseHash(byte[] passphraseHash)
        {
            var entropy = HashUtility.GetSalt(16);

            var cipherValue = _dataProtector.Protect(passphraseHash, entropy);

            var registryValueBytes = new byte[16 + cipherValue.Length];

            Array.Copy(entropy, 0, registryValueBytes, 0, 16);
            Array.Copy(cipherValue, 0, registryValueBytes, 16, cipherValue.Length);

            var registryValue = Convert.ToBase64String(registryValueBytes);

            _registryStore.Write(KeyName, registryValue);
        }

        /// <summary>
        /// Reads the passphrase hash.
        /// </summary>
        /// <returns>The passphrase hash.</returns>
        internal byte[] ReadPassphraseHash()
        {
            var registryValue = _registryStore.Read(KeyName);

            if (registryValue == null)
            {
                return new byte[] { };
            }

            var registryValueBytes = Convert.FromBase64String(registryValue);

            var entropy = new byte[16];

            var cipherValue = new byte[registryValueBytes.Length - 16];

            Array.Copy(registryValueBytes, 0, entropy, 0, 16);
            Array.Copy(registryValueBytes, 16, cipherValue, 0, cipherValue.Length);

            var passphraseHash = _dataProtector.Unprotect(cipherValue, entropy);

            return passphraseHash;
        }

#pragma warning disable S3242 // Method parameters should be declared with base types

        // ReSharper disable once SuggestBaseTypeForParameter
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
            var iv = new byte[16];

            Array.Copy(passphraseHash, 0, key, 0, 16);
            Array.Copy(passphraseHash, 0, iv, 0, 16); // TODO: Generate/save/load IV seperate from passphrase (key).

            var algorithm = Rijndael.Create();

            algorithm.KeySize = 128;
            algorithm.BlockSize = 128;
            algorithm.Key = key;
            algorithm.IV = iv;

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
            return GetAlgorithm(ReadPassphraseHash());
        }
    }
}