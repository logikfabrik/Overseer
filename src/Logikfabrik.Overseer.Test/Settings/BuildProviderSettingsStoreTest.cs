// <copyright file="BuildProviderSettingsStoreTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Overseer.Settings;

    [TestClass]
    public class BuildProviderSettingsStoreTest
    {
        [TestMethod]
        public void BuildProviderSettingsStore_CanSave()
        {
            var settings = new BuildProviderSettings[] { };

            var serializerMock = new Mock<IBuildProviderSettingsSerializer>();
            var fileStoreMock = new Mock<IFileStore>();

            var store = new BuildProviderSettingsStore(serializerMock.Object, fileStoreMock.Object);

            store.SaveAsync(settings).Wait();
        }

        [TestMethod]
        public void BuildProviderSettingsStore_CanLoad()
        {
            var serializerMock = new Mock<IBuildProviderSettingsSerializer>();
            var fileStoreMock = new Mock<IFileStore>();

            var store = new BuildProviderSettingsStore(serializerMock.Object, fileStoreMock.Object);

            var settings = store.LoadAsync().Result;

            Assert.IsNotNull(settings);
        }
    }
}
