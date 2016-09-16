// <copyright file="BuildProviderSettingsStoreTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Overseer.Settings;

    [TestClass]
    public class BuildProviderSettingsStoreTest
    {
        [TestMethod]
        public void BuildProviderSettingsStore_CanSave()
        {
            var settings = new[]
            {
                new BuildProviderSettings
                {
                    Name = "Name",
                    BuildProviderTypeName = typeof(object).AssemblyQualifiedName,
                    Settings = new[]
                    {
                        new Setting { Name = "Name", Value = "Value" }
                    }
                }
            };

            var store = new BuildProviderSettingsStore();

            store.SaveAsync(settings).Wait();
        }

        [TestMethod]
        public void BuildProviderSettingsStore_CanLoad()
        {
            var store = new BuildProviderSettingsStore();

            var settings = store.LoadAsync().Result;

            Assert.IsNotNull(settings);
        }
    }
}
