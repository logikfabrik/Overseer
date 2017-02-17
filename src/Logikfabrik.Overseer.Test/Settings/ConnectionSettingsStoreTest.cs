// <copyright file="ConnectionSettingsStoreTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using Moq;
    using Overseer.Settings;
    using Xunit;

    public class ConnectionSettingsStoreTest
    {
        [Fact]
        public void CanSave()
        {
            var settings = new ConnectionSettings[] { };

            var serializerMock = new Mock<IConnectionSettingsSerializer>();
            var fileStoreMock = new Mock<IFileStore>();

            var store = new ConnectionSettingsStore(serializerMock.Object, fileStoreMock.Object);

            store.Save(settings);

            fileStoreMock.Verify(m => m.Write(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void CanLoad()
        {
            var serializerMock = new Mock<IConnectionSettingsSerializer>();
            var fileStoreMock = new Mock<IFileStore>();

            var store = new ConnectionSettingsStore(serializerMock.Object, fileStoreMock.Object);

            store.Load();

            fileStoreMock.Verify(m => m.Read(), Times.Once);
        }
    }
}
