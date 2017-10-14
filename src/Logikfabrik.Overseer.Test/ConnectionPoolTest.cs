// <copyright file="ConnectionPoolTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test
{
    using System;
    using System.Linq;
    using Moq;
    using Overseer.Settings;
    using Ploeh.AutoFixture.Xunit2;
    using Settings;
    using Shouldly;
    using Xunit;

    public class ConnectionPoolTest
    {
        [Fact]
        public void CanGet()
        {
            var settingsStoreMock = new Mock<IConnectionSettingsStore>();

            settingsStoreMock.Setup(m => m.Load()).Returns(new ConnectionSettings[]
            {
                new ConnectionSettingsA(),
                new ConnectionSettingsB()
            });

            var repository = new ConnectionSettingsRepository(settingsStoreMock.Object);

            var factoryMock = new Mock<IBuildProviderStrategy>();

            var connectionPool = new ConnectionPool(repository, factoryMock.Object);

            connectionPool.CurrentConnections.Count().ShouldBe(2);
        }

        [Theory]
        [AutoData]
        public void CanUpdate(Guid id, string nameBeforeUpdate, string nameAfterUpdate)
        {
            var settingsBeforeUpdate = new ConnectionSettingsA { Id = id, Name = nameBeforeUpdate };

            var settingsStoreMock = new Mock<IConnectionSettingsStore>();

            settingsStoreMock.Setup(m => m.Load()).Returns(new ConnectionSettings[] { settingsBeforeUpdate });

            var repository = new ConnectionSettingsRepository(settingsStoreMock.Object);

            var factoryMock = new Mock<IBuildProviderStrategy>();

            var connectionPool = new ConnectionPool(repository, factoryMock.Object);

            var settingsToUpdate = new ConnectionSettingsA { Id = id, Name = nameAfterUpdate };

            repository.Update(settingsToUpdate);

            var connection = connectionPool.CurrentConnections.Single();

            connection.Settings.Name.ShouldBe(nameAfterUpdate);
        }

        [Fact]
        public void CanRemove()
        {
            var settingsA = new ConnectionSettingsA();

            var settingsStoreMock = new Mock<IConnectionSettingsStore>();

            settingsStoreMock.Setup(m => m.Load()).Returns(new ConnectionSettings[] { settingsA });

            var repository = new ConnectionSettingsRepository(settingsStoreMock.Object);

            var factoryMock = new Mock<IBuildProviderStrategy>();

            var connectionPool = new ConnectionPool(repository, factoryMock.Object);

            repository.Remove(settingsA.Id);

            connectionPool.CurrentConnections.Count().ShouldBe(0);
        }

        [Fact]
        public void CanAdd()
        {
            var settingsStoreMock = new Mock<IConnectionSettingsStore>();

            settingsStoreMock.Setup(m => m.Load()).Returns(new ConnectionSettings[] { });

            var repository = new ConnectionSettingsRepository(settingsStoreMock.Object);

            var factoryMock = new Mock<IBuildProviderStrategy>();

            var connectionPool = new ConnectionPool(repository, factoryMock.Object);

            repository.Add(new ConnectionSettingsA());

            connectionPool.CurrentConnections.Count().ShouldBe(1);
        }
    }
}
