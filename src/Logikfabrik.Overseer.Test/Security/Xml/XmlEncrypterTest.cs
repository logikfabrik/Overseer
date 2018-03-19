// <copyright file="XmlEncrypterTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Security.Xml
{
    using System;
    using System.Xml;
    using Moq;
    using Moq.AutoMock;
    using Overseer.IO.Registry;
    using Overseer.Security;
    using Overseer.Security.Xml;
    using Ploeh.AutoFixture.Xunit2;
    using Shouldly;
    using Xunit;

    public class XmlEncrypterTest
    {
        [Theory]
        [InlineAutoData(16)]
        [InlineAutoData(32)]
        public void CanSetPassphrase(int size, string passphrase)
        {
            var mocker = new AutoMocker();

            var xmlEncrypter = mocker.CreateInstance<XmlEncrypter>();

            var salt = HashUtility.GetSalt(size);

            var dataProtectorMock = mocker.GetMock<IDataProtector>();

            dataProtectorMock.Setup(m => m.Protect(It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns((byte[] userData, byte[] entropy) => userData);

            var registryStoreMock = mocker.GetMock<IRegistryStore>();

            xmlEncrypter.SetPassphrase(passphrase, salt);

            registryStoreMock.Verify(m => m.Write(XmlEncrypter.KeyName, It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public void CanWritePassphraseHash(string passphrase)
        {
            var mocker = new AutoMocker();

            var xmlEncrypter = mocker.CreateInstance<XmlEncrypter>();

            var passphraseHash = HashUtility.GetHash(passphrase, HashUtility.GetSalt(16), 32);

            var dataProtectorMock = mocker.GetMock<IDataProtector>();

            dataProtectorMock.Setup(m => m.Protect(It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns((byte[] userData, byte[] entropy) => userData);

            var registryStoreMock = mocker.GetMock<IRegistryStore>();

            xmlEncrypter.WritePassphraseHash(passphraseHash);

            registryStoreMock.Verify(m => m.Write(XmlEncrypter.KeyName, It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public void CanReadPassphraseHash(string passphrase)
        {
            var mocker = new AutoMocker();

            var xmlEncrypter = mocker.CreateInstance<XmlEncrypter>();

            var dataProtectorMock = mocker.GetMock<IDataProtector>();

            dataProtectorMock.Setup(m => m.Unprotect(It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns((byte[] encryptedData, byte[] entropy) => encryptedData);

            var registryStoreMock = mocker.GetMock<IRegistryStore>();

            registryStoreMock.Setup(m => m.Read(XmlEncrypter.KeyName)).Returns(Convert.ToBase64String(HashUtility.GetHash(passphrase, HashUtility.GetSalt(16), 32)));

            var passphraseHash = xmlEncrypter.ReadPassphraseHash();

            passphraseHash.ShouldNotBeEmpty();
        }

        [Fact]
        public void CanEncrypt()
        {
            // TODO: This unit test.
        }

        [Theory]
        [AutoData]
        public void CanDecrypt(string passphrase)
        {
            var mocker = new AutoMocker();

            var xmlEncrypter = mocker.CreateInstance<XmlEncrypter>();

            var dataProtectorMock = mocker.GetMock<IDataProtector>();

            dataProtectorMock.Setup(m => m.Unprotect(It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns((byte[] encryptedData, byte[] entropy) => encryptedData);

            var registryStoreMock = mocker.GetMock<IRegistryStore>();

            registryStoreMock.Setup(m => m.Read(XmlEncrypter.KeyName)).Returns(Convert.ToBase64String(HashUtility.GetHash(passphrase, HashUtility.GetSalt(16), 32)));

            var xmlDocument = xmlEncrypter.Decrypt(new XmlDocument(), new[] { "EncryptedData" });

            // TODO: Coverage.
            xmlDocument.ShouldNotBeNull();
        }
    }
}