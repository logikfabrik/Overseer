// <copyright file="ConnectionTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test
{
    using System;
    using Moq;
    using Settings;
    using Xunit;

    public class ConnectionTest
    {
        [Fact]
        public void CanNotChangeSettingsToSettingsWithNewId()
        {
            var factoryMock = new Mock<IBuildProviderFactory>();

            var settings1 = new ConnectionSettingsA();

            var connection = new Connection(factoryMock.Object, settings1);

            var settings2 = new ConnectionSettingsA();

            Assert.Throws<ArgumentException>(() => connection.Settings = settings2);
        }

        [Fact]
        public void CanChangeSettingsToSettingsWithNewName()
        {
            var factoryMock = new Mock<IBuildProviderFactory>();

            var settings1 = new ConnectionSettingsA { Name = "Old name" };

            var connection = new Connection(factoryMock.Object, settings1);

            var settings2 = new ConnectionSettingsA { Id = settings1.Id, Name = "New name" };

            connection.Settings = settings2;

            Assert.Equal("New name", connection.Settings.Name);
        }
    }
}
