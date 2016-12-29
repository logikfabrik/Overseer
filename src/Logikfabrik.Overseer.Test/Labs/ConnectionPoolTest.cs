namespace Logikfabrik.Overseer.Test.Labs
{
    using System;
    using System.Linq;
    using Moq;
    using Overseer.Labs;
    using Overseer.Settings;
    using Settings;
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

            var connectionPool = new ConnectionPool(repository);

            Assert.Equal(2, connectionPool.CurrentConnections.Count());
        }

        [Fact]
        public void CanUpdate()
        {
            var id = Guid.NewGuid();

            var settings1 = new ConnectionSettingsA { Id = id, Name = "My Settings" };

            var settingsStoreMock = new Mock<IConnectionSettingsStore>();

            settingsStoreMock.Setup(m => m.Load()).Returns(new ConnectionSettings[] { settings1 });

            var repository = new ConnectionSettingsRepository(settingsStoreMock.Object);

            var connectionPool = new ConnectionPool(repository);

            var settings2 = new ConnectionSettingsA { Id = id, Name = "Your Settings" };

            repository.Update(settings2);

            Assert.Equal(settings2.Name, connectionPool.CurrentConnections.Single().Settings.Name);
        }

        [Fact]
        public void CanRemove()
        {
            var settingsA = new ConnectionSettingsA();

            var settingsStoreMock = new Mock<IConnectionSettingsStore>();

            settingsStoreMock.Setup(m => m.Load()).Returns(new ConnectionSettings[] { settingsA });

            var repository = new ConnectionSettingsRepository(settingsStoreMock.Object);

            var connectionPool = new ConnectionPool(repository);

            repository.Remove(settingsA.Id);

            Assert.Equal(0, connectionPool.CurrentConnections.Count());
        }

        [Fact]
        public void CanAdd()
        {
            var settingsStoreMock = new Mock<IConnectionSettingsStore>();

            settingsStoreMock.Setup(m => m.Load()).Returns(new ConnectionSettings[] { });

            var repository = new ConnectionSettingsRepository(settingsStoreMock.Object);

            var connectionPool = new ConnectionPool(repository);

            repository.Add(new ConnectionSettingsA());

            Assert.Equal(1, connectionPool.CurrentConnections.Count());
        }
    }
}
