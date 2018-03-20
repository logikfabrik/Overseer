// <copyright file="XmlEncrypterTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Security.Xml
{
    using System.Xml;
    using Moq.AutoMock;
    using Overseer.Passphrase;
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

            var passphraseRepositoryMock = mocker.GetMock<IPassphraseRepository>();

            passphraseRepositoryMock.Setup(m => m.ReadHash()).Returns(PassphraseUtility.GetHash(passphrase));

            var xmlDocument = xmlEncrypter.Decrypt(new XmlDocument(), new[] { "EncryptedData" });

            // TODO: Coverage.
            xmlDocument.ShouldNotBeNull();
        }
    }
}