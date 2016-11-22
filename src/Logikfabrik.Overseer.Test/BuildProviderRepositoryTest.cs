// <copyright file="BuildProviderRepositoryTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test
{
    using System.Linq;
    using Moq;
    using Overseer.Settings;
    using Settings;
    using Xunit;

    public class BuildProviderRepositoryTest
    {
        [Fact]
        public void CanGetAll()
        {
            var settingsRepositoryMock = new Mock<IConnectionSettingsRepository>();

            settingsRepositoryMock.Setup(m => m.GetAll()).Returns(new ConnectionSettings[] { new ConnectionSettingsA(), new ConnectionSettingsB() });

            var repository = new BuildProviderRepository(settingsRepositoryMock.Object);

            var providers = repository.GetAll();

            Assert.Equal(2, providers.Count());
        }
    }
}
