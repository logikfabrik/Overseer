// <copyright file="ConnectionSettingsStoreTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using Moq;
    using Moq.AutoMock;
    using Overseer.IO;
    using Overseer.Settings;
    using Ploeh.AutoFixture.Xunit2;
    using Xunit;

    public class ConnectionSettingsStoreTest
    {
        [Fact]
        public void WillEncryptOnSave()
        {
            var mocker = new AutoMocker();

            var store = mocker.CreateInstance<ConnectionSettingsStore>();

            var encrypterMock = mocker.GetMock<IConnectionSettingsEncrypter>();

            store.Save(new ConnectionSettings[] { });

            encrypterMock.Verify(m => m.Encrypt(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void WillWiteOnSave()
        {
            var mocker = new AutoMocker();

            var store = mocker.CreateInstance<ConnectionSettingsStore>();

            var fileStoreMock = mocker.GetMock<IFileStore>();

            store.Save(new ConnectionSettings[] { });

            fileStoreMock.Verify(m => m.Write(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public void WillDecryptOnLoad(string xml)
        {
            var mocker = new AutoMocker();

            var store = mocker.CreateInstance<ConnectionSettingsStore>();

            var fileStoreMock = mocker.GetMock<IFileStore>();

            fileStoreMock.Setup(m => m.Read()).Returns(xml);

            var encrypterMock = mocker.GetMock<IConnectionSettingsEncrypter>();

            store.Load();

            encrypterMock.Verify(m => m.Decrypt(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void WillReadOnLoad()
        {
            var mocker = new AutoMocker();

            var store = mocker.CreateInstance<ConnectionSettingsStore>();

            var fileStoreMock = mocker.GetMock<IFileStore>();

            store.Load();

            fileStoreMock.Verify(m => m.Read(), Times.Once);
        }
    }
}
