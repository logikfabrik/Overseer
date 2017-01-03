// <copyright file="Module.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor
{
    using Ninject.Modules;
    using ViewModels;

    /// <summary>
    /// The <see cref="Module" /> class.
    /// </summary>
    public class Module : NinjectModule
    {
        /// <summary>
        /// Gets the module name.
        /// </summary>
        /// <value>
        /// The module name.
        /// </value>
        public override string Name { get; } = ModuleHelper.GetModuleName();

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<WPF.ViewModels.BuildProviderViewModel>().To<BuildProviderViewModel>().Named(Name);
            Bind<WPF.ViewModels.ConnectionViewModel>().To<ConnectionViewModel>().Named(Name);
            Bind<Settings.ConnectionSettings>().To<ConnectionSettings>().Named(Name);
        }
    }
}
