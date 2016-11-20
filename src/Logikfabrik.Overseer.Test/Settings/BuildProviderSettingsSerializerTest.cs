// <copyright file="BuildProviderSettingsSerializerTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Overseer.Settings;

    [TestClass]
    public class BuildProviderSettingsSerializerTest
    {
        [TestMethod]
        public void BuildProviderSettingsSerializer_CanDeserialize()
        {
            var supportedTypes = new[]
            {
                typeof(BuildProviderSettingsA),
                typeof(BuildProviderSettingsB)
            };

            var serializer = new BuildProviderSettingsSerializer(supportedTypes);

            var settings = serializer.Serialize(new BuildProviderSettings[]
            {
                new BuildProviderSettingsA { SettingA = "SettingA" },
                new BuildProviderSettingsB { SettingB = "SettingB" }
            });

            Assert.IsNotNull(serializer.Deserialize(settings));
        }

        [TestMethod]
        public void BuildProviderSettingsSerializer_CanSerialize()
        {
            var supportedTypes = new[]
            {
                typeof(BuildProviderSettingsA),
                typeof(BuildProviderSettingsB)
            };

            var serializer = new BuildProviderSettingsSerializer(supportedTypes);

            var settings = serializer.Serialize(new BuildProviderSettings[]
            {
                new BuildProviderSettingsA { SettingA = "SettingA" },
                new BuildProviderSettingsB { SettingB = "SettingB" }
            });

            Assert.IsNotNull(settings);
        }
    }
}