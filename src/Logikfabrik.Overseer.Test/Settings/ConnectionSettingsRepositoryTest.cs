// <copyright file="ConnectionSettingsRepositoryTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using System;
    using AutoFixture.Xunit2;
    using Moq;
    using Moq.AutoMock;
    using Overseer.Settings;
    using Shouldly;
    using Xunit;

    public class ConnectionSettingsRepositoryTest
    {
        [Fact]
        public void CanAdd()
        {
            var mocker = new AutoMocker();

            var repository = mocker.CreateInstance<ConnectionSettingsRepository>();

            var settings = GetSettingsMock().Object;

            repository.Add(settings);

            repository.Get(settings.Id).ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void CanUpdate(Guid id, string nameBeforeUpdate, string nameAfterUpdate)
        {
            var mocker = new AutoMocker();

            var repository = mocker.CreateInstance<ConnectionSettingsRepository>();

            var settingsBeforeUpdate = GetSettingsMock().Object;

            settingsBeforeUpdate.Id = id;
            settingsBeforeUpdate.Name = nameBeforeUpdate;

            repository.Add(settingsBeforeUpdate);

            var settingsToUpdate = repository.Get(id);

            settingsToUpdate.Name = nameAfterUpdate;

            repository.Update(settingsToUpdate);

            var settingsAfterUpdate = repository.Get(id);

            settingsAfterUpdate.Name.ShouldBe(nameAfterUpdate);
        }

        [Fact]
        public void CanRemove()
        {
            var mocker = new AutoMocker();

            var repository = mocker.CreateInstance<ConnectionSettingsRepository>();

            var settings = GetSettingsMock().Object;

            repository.Add(settings);
            repository.Remove(settings.Id);

            repository.Get(settings.Id).ShouldBeNull();
        }

        [Fact]
        public void WillCloneOnAdd()
        {
            var mocker = new AutoMocker();

            var repository = mocker.CreateInstance<ConnectionSettingsRepository>();

            var settingsMock = GetSettingsMock();

            repository.Add(settingsMock.Object);

            settingsMock.Verify(m => m.Clone(), Times.Once);
        }

        [Fact]
        public void WillCloneOnUpdate()
        {
            var mocker = new AutoMocker();

            var repository = mocker.CreateInstance<ConnectionSettingsRepository>();

            var settingsMock = GetSettingsMock();

            repository.Add(settingsMock.Object);
            repository.Update(settingsMock.Object);

            settingsMock.Verify(m => m.Clone(), Times.Exactly(2));
        }

        private static Mock<ConnectionSettings> GetSettingsMock()
        {
            var settingsMock = new Mock<ConnectionSettings>();

            settingsMock.Setup(m => m.Clone()).Returns(() => Clone(settingsMock));

            return settingsMock;
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
