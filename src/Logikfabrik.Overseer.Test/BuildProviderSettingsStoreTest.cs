using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logikfabrik.Overseer.Test
{
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
                    ProviderType = typeof (object),
                    Settings = new[]
                    {
                        new Setting { Name = "Name", Value = "Value"}
                    }
                }
            };

            var store = new BuildProviderSettingsStore();

            store.SaveAsync(settings);
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
