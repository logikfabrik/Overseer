// <copyright file="ConnectionSettingsRepositoryTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using System;
    using Moq;
    using Overseer.Settings;
    using Ploeh.AutoFixture.Xunit2;
    using Xunit;

    public class ConnectionSettingsRepositoryTest
    {
        [Fact]
        public void CanAdd()
        {
            var settingsMock = new Mock<ConnectionSettings>();

            settingsMock.Setup(m => m.Clone()).Returns(() => Clone(settingsMock));

            var repository = new ConnectionSettingsRepository(new Mock<IConnectionSettingsStore>().Object);

            repository.Add(settingsMock.Object);

            Assert.NotNull(repository.Get(settingsMock.Object.Id));
        }

        [Theory]
        [AutoData]
        public void CanUpdate(Guid id, string nameBeforeUpdate, string nameAfterUpdate)
        {
            var settingsBeforeUpdateMock = new Mock<ConnectionSettings>();

            settingsBeforeUpdateMock.Object.Id = id;
            settingsBeforeUpdateMock.Object.Name = nameBeforeUpdate;
            settingsBeforeUpdateMock.Setup(m => m.Clone()).Returns(() => Clone(settingsBeforeUpdateMock));

            var repository = new ConnectionSettingsRepository(new Mock<IConnectionSettingsStore>().Object);

            repository.Add(settingsBeforeUpdateMock.Object);

            var settingsToUpdate = repository.Get(id);

            settingsToUpdate.Name = nameAfterUpdate;

            repository.Update(settingsToUpdate);

            var settingsAfterUpdate = repository.Get(id);

            Assert.Equal(nameAfterUpdate, settingsAfterUpdate.Name);
        }

        [Fact]
        public void CanRemove()
        {
            var settingsMock = new Mock<ConnectionSettings>();

            settingsMock.Setup(m => m.Clone()).Returns(() => Clone(settingsMock));

            var repository = new ConnectionSettingsRepository(new Mock<IConnectionSettingsStore>().Object);

            repository.Add(settingsMock.Object);
            repository.Remove(settingsMock.Object.Id);

            Assert.Null(repository.Get(settingsMock.Object.Id));
        }

        [Fact]
        public void WillCloneOnAdd()
        {
            var settingsMock = new Mock<ConnectionSettings>();

            settingsMock.Setup(m => m.Clone()).Returns(() => Clone(settingsMock));

            var repository = new ConnectionSettingsRepository(new Mock<IConnectionSettingsStore>().Object);

            repository.Add(settingsMock.Object);

            settingsMock.Verify(m => m.Clone(), Times.Once);
        }

        [Fact]
        public void WillCloneOnUpdate()
        {
            var settingsMock = new Mock<ConnectionSettings>();

            settingsMock.Setup(m => m.Clone()).Returns(() => Clone(settingsMock));

            var repository = new ConnectionSettingsRepository(new Mock<IConnectionSettingsStore>().Object);

            repository.Add(settingsMock.Object);
            repository.Update(settingsMock.Object);

            settingsMock.Verify(m => m.Clone(), Times.Exactly(2));
        }

        private static ConnectionSettings Clone(IMock<ConnectionSettings> settingsMock)
        {
            var cloneMock = new Mock<ConnectionSettings>();

            cloneMock.Object.Id = settingsMock.Object.Id;

            if (!string.IsNullOrWhiteSpace(settingsMock.Object.Name))
            {
                cloneMock.Object.Name = settingsMock.Object.Name;
            }

            cloneMock.Setup(m => m.Clone()).Returns(() => Clone(cloneMock));

            return cloneMock.Object;
        }
    }
}
