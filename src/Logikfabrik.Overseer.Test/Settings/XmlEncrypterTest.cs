// <copyright file="XmlEncrypterTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using System;
    using System.Xml;
    using Moq;
    using Moq.AutoMock;
    using Overseer.Settings;
    using Ploeh.AutoFixture.Xunit2;
    using Shouldly;
    using Xunit;

    public class XmlEncrypterTest
    {
        [Theory]
        [InlineAutoData(16)]
        [InlineAutoData(32)]
        public void CanSetPassPhrase(int size, string passPhrase)
        {
            var mocker = new AutoMocker();

            var xmlEncrypter = mocker.CreateInstance<XmlEncrypter>();

            var salt = HashUtility.GetSalt(size);

            var dataProtectorMock = mocker.GetMock<IDataProtector>();

            dataProtectorMock.Setup(m => m.Protect(It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns((byte[] userData, byte[] entropy) => userData);

            var registryStoreMock = mocker.GetMock<IRegistryStore>();

            xmlEncrypter.SetPassPhrase(passPhrase, salt);

            registryStoreMock.Verify(m => m.Write(XmlEncrypter.KeyName, It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public void CanWritePassPhraseHash(string passPhrase)
        {
            var mocker = new AutoMocker();

            var xmlEncrypter = mocker.CreateInstance<XmlEncrypter>();

            var passPhraseHash = HashUtility.GetHash(passPhrase, HashUtility.GetSalt(16), 32);

            var dataProtectorMock = mocker.GetMock<IDataProtector>();

            dataProtectorMock.Setup(m => m.Protect(It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns((byte[] userData, byte[] entropy) => userData);

            var registryStoreMock = mocker.GetMock<IRegistryStore>();

            xmlEncrypter.WritePassPhraseHash(passPhraseHash);

            registryStoreMock.Verify(m => m.Write(XmlEncrypter.KeyName, It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public void CanReadPassPhraseHash(string passPhrase)
        {
            var mocker = new AutoMocker();

            var xmlEncrypter = mocker.CreateInstance<XmlEncrypter>();

            var dataProtectorMock = mocker.GetMock<IDataProtector>();

            dataProtectorMock.Setup(m => m.Unprotect(It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns((byte[] encryptedData, byte[] entropy) => encryptedData);

            var registryStoreMock = mocker.GetMock<IRegistryStore>();

            registryStoreMock.Setup(m => m.Read(XmlEncrypter.KeyName)).Returns(Convert.ToBase64String(HashUtility.GetHash(passPhrase, HashUtility.GetSalt(16), 32)));

            var passPhraseHash = xmlEncrypter.ReadPassPhraseHash();

            passPhraseHash.ShouldNotBeEmpty();
        }

        [Fact]
        public void CanEncrypt()
        {
            // TODO: This unit test.
        }

        [Theory]
        [AutoData]
        public void CanDecrypt(string passPhrase)
        {
            var mocker = new AutoMocker();

            var xmlEncrypter = mocker.CreateInstance<XmlEncrypter>();

            var dataProtectorMock = mocker.GetMock<IDataProtector>();

            dataProtectorMock.Setup(m => m.Unprotect(It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns((byte[] encryptedData, byte[] entropy) => encryptedData);

            var registryStoreMock = mocker.GetMock<IRegistryStore>();

            registryStoreMock.Setup(m => m.Read(XmlEncrypter.KeyName)).Returns(Convert.ToBase64String(HashUtility.GetHash(passPhrase, HashUtility.GetSalt(16), 32)));

            var xmlDocument = xmlEncrypter.Decrypt(new XmlDocument(), new[] { "EncryptedData" });

            // TODO: Coverage.
            xmlDocument.ShouldNotBeNull();
        }
    }
}