// <copyright file="BuildProviderRepositoryTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Overseer.Settings;
    using Settings;

    [TestClass]
    public class BuildProviderRepositoryTest
    {
        [TestMethod]
        public void BuildProviderRepository_CanGetAll()
        {
            var settingsRepositoryMock = new Mock<IConnectionSettingsRepository>();

            settingsRepositoryMock.Setup(m => m.GetAll()).Returns(new ConnectionSettings[] { new ConnectionSettingsA(), new ConnectionSettingsB() });

            var repository = new BuildProviderRepository(settingsRepositoryMock.Object);

            var providers = repository.GetAll();

            Assert.AreEqual(2, providers.Count());
        }
    }
}
