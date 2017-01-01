// <copyright file="ConnectionSettingsTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using System;
    using Xunit;

    public class ConnectionSettingsTest
    {
        [Fact]
        public void AreEqual()
        {
            var id = Guid.NewGuid();

            var settings1 = new ConnectionSettingsA { Id = id, Name = "My Settings" };
            var settings2 = new ConnectionSettingsA { Id = id, Name = "My Settings" };

            Assert.Equal(settings1, settings2);
        }

        [Fact]
        public void AreNotEqual()
        {
            var id = Guid.NewGuid();

            var settings1 = new ConnectionSettingsA { Id = id, Name = "My Settings" };
            var settings2 = new ConnectionSettingsA { Id = id, Name = "Your Settings" };

            Assert.NotEqual(settings1, settings2);
        }

        [Fact]
        public void HaveEqualHashCodes()
        {
            var id = Guid.NewGuid();

            var hashCode1 = new ConnectionSettingsA { Id = id, Name = "My Settings" }.GetHashCode();
            var hashCode2 = new ConnectionSettingsA { Id = id, Name = "My Settings" }.GetHashCode();

            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void HaveNotEqualHashCodes()
        {
            var id = Guid.NewGuid();

            var hashCode1 = new ConnectionSettingsA { Id = id, Name = "My Settings" }.GetHashCode();
            var hashCode2 = new ConnectionSettingsA { Id = id, Name = "Your Settings" }.GetHashCode();

            Assert.NotEqual(hashCode1, hashCode2);
        }
    }
}