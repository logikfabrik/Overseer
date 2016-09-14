// <copyright file="Module.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using Ninject.Modules;
    using Settings;

    /// <summary>
    /// The <see cref="Module" /> class.
    /// </summary>
    /// <seealso cref="NinjectModule" />
    public class Module : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IBuildProviderSettingsStore>().To<BuildProviderSettingsStore>().InSingletonScope();
            Bind<IBuildProviderSettingsRepository>().To<BuildProviderSettingsRepository>().InSingletonScope();
            Bind<IBuildProviderRepository>().To<BuildProviderRepository>().InSingletonScope();
            Bind<IBuildMonitor>().To<BuildMonitor>().InSingletonScope();
        }
    }
}
