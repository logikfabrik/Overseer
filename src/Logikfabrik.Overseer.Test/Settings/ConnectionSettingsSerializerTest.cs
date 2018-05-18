// <copyright file="ConnectionSettingsSerializerTest.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using Overseer.Settings;
    using Shouldly;
    using Xunit;

    public class ConnectionSettingsSerializerTest
    {
        [Fact]
        public void CanDeserialize()
        {
            var supportedTypes = new[]
            {
                typeof(ConnectionSettingsA),
                typeof(ConnectionSettingsB)
            };

            var serializer = new ConnectionSettingsSerializer(supportedTypes);

            var settings = serializer.Serialize(new ConnectionSettings[]
            {
                new ConnectionSettingsA { SettingA = "SettingA", BuildsPerProject = 5 },
                new ConnectionSettingsB { SettingB = "SettingB", BuildsPerProject = 5 }
            });

            serializer.Deserialize(settings).Length.ShouldBe(2);
        }

        [Fact]
        public void CanSerialize()
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

            settings.ShouldNotBeNull();
        }
    }
}