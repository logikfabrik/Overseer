// <copyright file="XmlEncrypterTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using System;
    using Moq;
    using Overseer.Settings;
    using Ploeh.AutoFixture.Xunit2;
    using Xunit;

    public class XmlEncrypterTest
    {
        [Theory]
        [InlineAutoData(16)]
        [InlineAutoData(32)]
        public void CanSetPassPhrase(int size, string passPhrase)
        {
            var dataProtectorMock = new Mock<IDataProtector>();

            dataProtectorMock.Setup(m => m.Protect(It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns((byte[] userData, byte[] entropy) => userData);

            var registryStoreMock = new Mock<IRegistryStore>();

            var xmlEncrypter = new XmlEncrypter(dataProtectorMock.Object, registryStoreMock.Object);

            var salt = HashUtility.GetSalt(size);

            xmlEncrypter.SetPassPhrase(passPhrase, salt);

            registryStoreMock.Verify(m => m.Write(XmlEncrypterKey.Name, It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public void CanWritePassPhraseHash(string passPhrase)
        {
            var dataProtectorMock = new Mock<IDataProtector>();

            dataProtectorMock.Setup(m => m.Protect(It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns((byte[] userData, byte[] entropy) => userData);

            var registryStoreMock = new Mock<IRegistryStore>();

            var xmlEncrypter = new XmlEncrypter(dataProtectorMock.Object, registryStoreMock.Object);

            var passPhraseHash = HashUtility.GetHash(passPhrase, HashUtility.GetSalt(16), 32);

            xmlEncrypter.WritePassPhraseHash(passPhraseHash);

            registryStoreMock.Verify(m => m.Write(XmlEncrypterKey.Name, It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public void CanReadPassPhraseHash(string passPhrase)
        {
            var dataProtectorMock = new Mock<IDataProtector>();

            dataProtectorMock.Setup(m => m.Unprotect(It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns((byte[] encryptedData, byte[] entropy) => encryptedData);

            var registryStoreMock = new Mock<IRegistryStore>();

            registryStoreMock.Setup(m => m.Read(XmlEncrypterKey.Name)).Returns(Convert.ToBase64String(HashUtility.GetHash(passPhrase, HashUtility.GetSalt(16), 32)));

            var xmlEncrypter = new XmlEncrypter(dataProtectorMock.Object, registryStoreMock.Object);

            var passPhraseHash = xmlEncrypter.ReadPassPhraseHash();

            Assert.NotEmpty(passPhraseHash);
        }

        //[Fact]
        public void CanEncrypt()
        {
            throw new NotImplementedException();
        }

        //[Fact]
        public void CanDecrypt()
        {
            throw new NotImplementedException();
        }
    }
}