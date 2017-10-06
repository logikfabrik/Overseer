// <copyright file="XmlEncrypterTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

using Ploeh.AutoFixture.Xunit2;

namespace Logikfabrik.Overseer.Test.Settings
{
    using Moq;
    using Overseer.Settings;
    using Xunit;

    public class XmlEncrypterTest
    {
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

        [Fact]
        public void CanReadPassPhraseHash()
        {
            var dataProtectorMock = new Mock<IDataProtector>();

            dataProtectorMock.Setup(m => m.Unprotect(It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns((byte[] encryptedData, byte[] entropy) => encryptedData);

            var registryStoreMock = new Mock<IRegistryStore>();

            registryStoreMock.Setup(m => m.Read(XmlEncrypterKey.Name)).Returns("OOXdCEW9XVjm0AhwZGI7ZgEAAADQjJ3fARXREYx6AMBPwpfrAQAAAMDS/gO4PMNGkJjVFCAGp/EAAAAAAgAAAAAAEGYAAAABAAAgAAAA9xzZ6/gd+YVubAGDZo8LfKMOE8zfXTs4d8EbpFVBpjMAAAAADoAAAAACAAAgAAAAmIpps3dMzaUtsJUD6Ps2nRJukNoSAyTeoycwDJQ1LVMwAAAAAp0owlWiJRH8LkT9jzESm9Q3wfHSInSir+ocdva35A66pdMsTuW9TeuaRTzF7RZ0QAAAAMjgQPCjnlvuTavlrP9MqKgao99bA4Tdy9OnHdIS/Tmb55B8WMXh0hK3SMZX9s5wveUspH37dmFNLZ7fwAOuYrU=");

            var xmlEncrypter = new XmlEncrypter(dataProtectorMock.Object, registryStoreMock.Object);

            var passPhraseHash = xmlEncrypter.ReadPassPhraseHash();

            Assert.NotEmpty(passPhraseHash);
        }
    }
}