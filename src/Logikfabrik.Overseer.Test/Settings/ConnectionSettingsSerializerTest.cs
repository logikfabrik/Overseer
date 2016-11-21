// <copyright file="ConnectionSettingsSerializerTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Overseer.Settings;

    [TestClass]
    public class ConnectionSettingsSerializerTest
    {
        [TestMethod]
        public void ConnectionSettingsSerializer_CanDeserialize()
        {
            var supportedTypes = new[]
            {
                typeof(ConnectionSettingsA),
                typeof(ConnectionSettingsB)
            };

            var serializer = new ConnectionSettingsSerializer(supportedTypes);

            var settings = serializer.Serialize(new ConnectionSettings[]
            {
                new ConnectionSettingsA { SettingA = "SettingA" },
                new ConnectionSettingsB { SettingB = "SettingB" }
            });

            Assert.IsNotNull(serializer.Deserialize(settings));
        }

        [TestMethod]
        public void ConnectionSettingsSerializer_CanSerialize()
        {
            var supportedTypes = new[]
            {
                typeof(ConnectionSettingsA),
                typeof(ConnectionSettingsB)
            };

            var serializer = new ConnectionSettingsSerializer(supportedTypes);

            var settings = serializer.Serialize(new ConnectionSettings[]
            {
                new ConnectionSettingsA { SettingA = "SettingA" },
                new ConnectionSettingsB { SettingB = "SettingB" }
            });

            Assert.IsNotNull(settings);
        }
    }
}