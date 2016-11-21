// <copyright file="ConnectionSettingsStoreTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Overseer.Settings;

    [TestClass]
    public class ConnectionSettingsStoreTest
    {
        [TestMethod]
        public void ConnectionSettingsStore_CanSave()
        {
            var settings = new ConnectionSettings[] { };

            var serializerMock = new Mock<IConnectionSettingsSerializer>();
            var fileStoreMock = new Mock<IFileStore>();

            var store = new ConnectionSettingsStore(serializerMock.Object, fileStoreMock.Object);

            store.SaveAsync(settings).Wait();

            fileStoreMock.Verify(m => m.Write(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void ConnectionSettingsStore_CanLoad()
        {
            var serializerMock = new Mock<IConnectionSettingsSerializer>();
            var fileStoreMock = new Mock<IFileStore>();

            var store = new ConnectionSettingsStore(serializerMock.Object, fileStoreMock.Object);

            var settings = store.LoadAsync().Result;

            Assert.IsNotNull(settings);

            fileStoreMock.Verify(m => m.Read(), Times.Once);
        }
    }
}
