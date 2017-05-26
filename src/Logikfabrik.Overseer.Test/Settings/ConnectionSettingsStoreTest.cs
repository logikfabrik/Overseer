// <copyright file="ConnectionSettingsStoreTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using Moq;
    using Overseer.Settings;
    using Ploeh.AutoFixture.Xunit2;
    using Xunit;

    public class ConnectionSettingsStoreTest
    {
        [Fact]
        public void CanSave()
        {
            var settings = new ConnectionSettings[] { };

            var serializerMock = new Mock<IConnectionSettingsSerializer>();
            var encrypterMock = new Mock<IConnectionSettingsEncrypter>();
            var fileStoreMock = new Mock<IFileStore>();

            var store = new ConnectionSettingsStore(serializerMock.Object, encrypterMock.Object, fileStoreMock.Object);

            store.Save(settings);

            encrypterMock.Verify(m => m.Encrypt(It.IsAny<string>()), Times.Once);
            fileStoreMock.Verify(m => m.Write(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public void CanLoad()
        {
            var serializerMock = new Mock<IConnectionSettingsSerializer>();
            var encrypterMock = new Mock<IConnectionSettingsEncrypter>();
            var fileStoreMock = new Mock<IFileStore>();

            fileStoreMock.Setup(m => m.Read()).Returns("XML");

            var store = new ConnectionSettingsStore(serializerMock.Object, encrypterMock.Object, fileStoreMock.Object);

            store.Load();

            fileStoreMock.Verify(m => m.Read(), Times.Once);
            encrypterMock.Verify(m => m.Decrypt(It.IsAny<string>()), Times.Once);
        }
    }
}
