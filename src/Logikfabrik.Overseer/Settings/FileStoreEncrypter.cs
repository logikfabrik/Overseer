// <copyright file="FileStoreEncrypter.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

using EnsureThat;

namespace Logikfabrik.Overseer.Settings
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Security.Cryptography.Xml;
    using System.Xml;

    /// <summary>
    /// The <see cref="FileStoreEncrypter" /> class.
    /// </summary>
    public class FileStoreEncrypter : IFileStoreEncrypter
    {
        private const string ContainerName = "DKDKDKD";

        public FileStoreEncrypter()
        {
        }


        public string Encrypt(string xml)
        {
            Ensure.That(xml).IsNotNullOrWhiteSpace();

            throw new NotImplementedException();
        }

        public string Decrypt(string xml)
        {
            Ensure.That(xml).IsNotNullOrWhiteSpace();

            throw new NotImplementedException();
        }

        private byte[] GetPassword()
        {
            
        }

        private byte[] GetSalt(string password)
        {
            const int size = 16;

            var salt = System.Text.Encoding.UTF8.GetBytes(password);

            if (salt.Length == size)
            {
                return salt;
            }

            if (salt.Length > size)
            {
                Array.Resize(ref salt, size);

                return salt;
            }

            return Enumerable.Repeat<byte>(0, size - salt.Length).Concat(salt).ToArray();
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        private static string Encrypt(string xml, Rijndael algorithm)
        {
            var document = new XmlDocument();

            document.LoadXml(xml);

            var tagNames = new[] { "Password", "Token" };

            foreach (var name in tagNames)
            {
                var elements = document.GetElementsByTagName(name);

                while (elements.Count > 0)
                {
                    var element = (XmlElement)elements[0];

                    var encryptedData = new EncryptedData
                    {
                        CipherData =
                        {
                            CipherValue = new EncryptedXml().EncryptData(element, algorithm, true)
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

                    EncryptedXml.ReplaceElement(element, encryptedData, true);

                    elements = document.GetElementsByTagName(name);
                }
            }

            return document.OuterXml;
        }

        private static string Decrypt(string xml, SymmetricAlgorithm algorithm)
        {
            var document = new XmlDocument();

            document.LoadXml(xml);

            var elements = document.GetElementsByTagName(nameof(EncryptedData));

            while (elements.Count > 0)
            {
                var element = (XmlElement)elements[0];

                var encryptedData = new EncryptedData();

                encryptedData.LoadXml(element);

                var encryptedXml = new EncryptedXml();

                var decryptedData = encryptedXml.DecryptData(encryptedData, algorithm);

                encryptedXml.ReplaceData(element, decryptedData);

                elements = document.GetElementsByTagName(nameof(EncryptedData));
            }

            return document.OuterXml;
        }
    }
}
