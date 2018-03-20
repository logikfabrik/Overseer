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
    using Overseer.Passphrase;
    using Overseer.Security;
    using Overseer.Security.Xml;
    using Ploeh.AutoFixture.Xunit2;
    using Shouldly;
    using Xunit;

    public class XmlEncrypterTest
    {
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

            registryStoreMock.Setup(m => m.Read(PassphraseRepository.KeyName)).Returns(Convert.ToBase64String(HashUtility.GetHash(passphrase, HashUtility.GetSalt(16), 32)));

            var xmlDocument = xmlEncrypter.Decrypt(new XmlDocument(), new[] { "EncryptedData" });

            // TODO: Coverage.
            xmlDocument.ShouldNotBeNull();
        }
    }
}