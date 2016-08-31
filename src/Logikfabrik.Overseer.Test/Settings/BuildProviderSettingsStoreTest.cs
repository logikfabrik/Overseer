using Logikfabrik.Overseer.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logikfabrik.Overseer.Test.Settings
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
                    ProviderTypeName = typeof (object).AssemblyQualifiedName,
                    Settings = new[]
                    {
                        new Setting { Name = "Name", Value = "Value"}
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
