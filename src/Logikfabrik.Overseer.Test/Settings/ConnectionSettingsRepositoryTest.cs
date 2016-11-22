// <copyright file="ConnectionSettingsRepositoryTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using System;
    using System.Linq;
    using Moq;
    using Overseer.Settings;
    using Xunit;

    public class ConnectionSettingsRepositoryTest
    {
        [Fact]
        public void CanAdd()
        {
            var settingsStoreMock = new Mock<IConnectionSettingsStore>();

            var repository = new ConnectionSettingsRepository(settingsStoreMock.Object);

            var settingsBeforeAdd = new ConnectionSettingsA();

            repository.Add(settingsBeforeAdd);

            var settingsAfterAdd = repository.Get(settingsBeforeAdd.Id);

            Assert.NotSame(settingsAfterAdd, settingsBeforeAdd);
            Assert.Equal(settingsAfterAdd.Id, settingsBeforeAdd.Id);
        }

        [Fact]
        public void CanRemove()
        {
            var settingsStoreMock = new Mock<IConnectionSettingsStore>();

            var repository = new ConnectionSettingsRepository(settingsStoreMock.Object);

            var settingsToAddAndRemove = new ConnectionSettingsA();

            repository.Add(settingsToAddAndRemove);
            repository.Remove(settingsToAddAndRemove.Id);

            Assert.Null(repository.Get(settingsToAddAndRemove.Id));
        }

        [Fact]
        public void CanUpdate()
        {
            var settingsStoreMock = new Mock<IConnectionSettingsStore>();

            var repository = new ConnectionSettingsRepository(settingsStoreMock.Object);

            var id = Guid.NewGuid();

            var settingToAdd = new ConnectionSettingsA { Id = id, Name = "Name before update" };

            repository.Add(settingToAdd);

            var settingsToUpdate = repository.Get(id);

            settingsToUpdate.Name = "Name after update";

            repository.Update(settingsToUpdate);

            var settingsAfterUpdate = repository.Get(id);

            Assert.Equal("Name after update", settingsAfterUpdate.Name);
        }

        [Fact]
        public void CanGetAll()
        {
            var settingsStoreMock = new Mock<IConnectionSettingsStore>();

            var repository = new ConnectionSettingsRepository(settingsStoreMock.Object);

            repository.Add(new ConnectionSettingsA());
            repository.Add(new ConnectionSettingsB());

            Assert.Equal(2, repository.GetAll().Count());
        }
    }
}
