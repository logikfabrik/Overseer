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
        public void CanSaveAsync()
        {
            var settings = new ConnectionSettings[] { };

            var serializerMock = new Mock<IConnectionSettingsSerializer>();
            var fileStoreMock = new Mock<IFileStore>();

            var store = new ConnectionSettingsStore(serializerMock.Object, fileStoreMock.Object);

            store.SaveAsync(settings).Wait();

            fileStoreMock.Verify(m => m.Write(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void CanLoadAsync()
        {
            var serializerMock = new Mock<IConnectionSettingsSerializer>();
            var fileStoreMock = new Mock<IFileStore>();

            var store = new ConnectionSettingsStore(serializerMock.Object, fileStoreMock.Object);

            store.LoadAsync().Wait();

            fileStoreMock.Verify(m => m.Read(), Times.Once);
        }
    }
}
