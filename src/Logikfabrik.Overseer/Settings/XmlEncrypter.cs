﻿// <copyright file="XmlEncrypter.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Security.Cryptography.Xml;
    using System.Xml;
    using EnsureThat;

    /// <summary>
    /// The <see cref="XmlEncrypter" /> class.
    /// </summary>
    public class XmlEncrypter : IXmlEncrypter
    {
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

            HasPassPhrase = ReadPassPhraseHash() != null;
        }

        /// <summary>
        /// Gets a value indicating whether this instance has a pass phrase.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has a pass phrase; otherwise, <c>false</c>.
        /// </value>
        public bool HasPassPhrase { get; private set; }

        /// <summary>
        /// Sets the pass phrase.
        /// </summary>
        /// <param name="passPhrase">The pass phrase.</param>
        /// <param name="salt">The salt.</param>
        public void SetPassPhrase(string passPhrase, byte[] salt)
        {
            Ensure.That(passPhrase).IsNotNullOrWhiteSpace();
            Ensure.That(salt).IsNotNull();
            Ensure.That(salt.Length % 16).Is(0);

            var passPhraseHash = HashHelper.GetHash(passPhrase, salt, 32);

            WritePassPhraseHash(passPhraseHash);

            HasPassPhrase = true;
        }

        /// <summary>
        /// Encrypts the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <param name="tagNames">The tag names of elements to encrypt.</param>
        /// <returns>
        /// The encrypted XML.
        /// </returns>
        public XmlDocument Encrypt(XmlDocument xml, string[] tagNames)
        {
            Ensure.That(xml).IsNotNull();
            Ensure.That(tagNames).IsNotNull();

            using (var algorithm = GetAlgorithm())
            {
                return Encrypt(xml, tagNames, algorithm);
            }
        }

        /// <summary>
        /// Decrypts the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <param name="tagNames">The tag names of elements to decrypt.</param>
        /// <returns>
        /// The decrypted XML.
        /// </returns>
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
        /// Writes the pass phrase hash.
        /// </summary>
        /// <param name="passPhraseHash">The pass phrase hash.</param>
        internal void WritePassPhraseHash(byte[] passPhraseHash)
        {
            var entropy = HashHelper.GetSalt(16);

            var cipherValue = _dataProtector.Protect(passPhraseHash, entropy);

            var registryValueBytes = new byte[16 + cipherValue.Length];

            Array.Copy(entropy, 0, registryValueBytes, 0, 16);
            Array.Copy(cipherValue, 0, registryValueBytes, 16, cipherValue.Length);

            var registryValue = Convert.ToBase64String(registryValueBytes);

            _registryStore.Write("PassPhrase", registryValue);
        }

        /// <summary>
        /// Reads the pass phrase hash.
        /// </summary>
        /// <returns>The pass phrase hash.</returns>
        internal byte[] ReadPassPhraseHash()
        {
            var registryValue = _registryStore.Read("PassPhrase");

            if (registryValue == null)
            {
                return null;
            }

            var registryValueBytes = Convert.FromBase64String(registryValue);

            var entropy = new byte[16];

            var cipherValue = new byte[registryValueBytes.Length - 16];

            Array.Copy(registryValueBytes, 0, entropy, 0, 16);
            Array.Copy(registryValueBytes, 16, cipherValue, 0, cipherValue.Length);

            var passPhraseHash = _dataProtector.Unprotect(cipherValue, entropy);

            return passPhraseHash;
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        private static XmlDocument Encrypt(XmlDocument xml, IEnumerable<string> tagNames, Rijndael algorithm)
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

                        case 192:
                            encryptedData.EncryptionMethod = new EncryptionMethod(EncryptedXml.XmlEncAES192Url);
                            break;

                        case 256:
                            encryptedData.EncryptionMethod = new EncryptionMethod(EncryptedXml.XmlEncAES256Url);
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

                    var encryptedData = new EncryptedData();

                    encryptedData.LoadXml(element);

                    var encryptedXml = new EncryptedXml();

                    var decryptedData = encryptedXml.DecryptData(encryptedData, algorithm);

                    encryptedXml.ReplaceData(element, decryptedData);

                    elements = xml.GetElementsByTagName(nameof(EncryptedData));
                }
            }

            return xml;
        }

        private static Rijndael GetAlgorithm(byte[] passPhraseHash)
        {
            var key = new byte[16];
            var iv = new byte[16];

            Array.Copy(passPhraseHash, 0, key, 0, 16);
            Array.Copy(passPhraseHash, 0, iv, 0, 16);

            var algorithm = Rijndael.Create();

            algorithm.KeySize = 128;
            algorithm.BlockSize = 128;
            algorithm.Key = key;
            algorithm.IV = iv;

            return algorithm;
        }

        private Rijndael GetAlgorithm()
        {
            return GetAlgorithm(ReadPassPhraseHash());
        }
    }
}