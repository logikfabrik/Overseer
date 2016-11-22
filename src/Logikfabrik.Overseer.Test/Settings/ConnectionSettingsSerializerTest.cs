// <copyright file="ConnectionSettingsSerializerTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using Overseer.Settings;
    using Xunit;

    public class ConnectionSettingsSerializerTest
    {
        [Fact]
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

            Assert.NotNull(serializer.Deserialize(settings));
        }

        [Fact]
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

            Assert.NotNull(settings);
        }
    }
}