// <copyright file="Module.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor
{
    using Ninject.Modules;
    using ViewModels;
    using ViewModels.Factories;

    /// <summary>
    /// The <see cref="Module" /> class.
    /// </summary>
    public class Module : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<WPF.ViewModels.BuildProviderViewModel>().To<BuildProviderViewModel>();
            Bind<WPF.ViewModels.ConnectionViewModel>().To<ConnectionViewModel>();
            Bind<Settings.ConnectionSettings>().To<ConnectionSettings>();
            Bind<WPF.ViewModels.Factories.IConnectionSettingsViewModelFactory<ConnectionSettings, ConnectionSettingsViewModel>>().To<WPF.ViewModels.Factories.ConnectionSettingsViewModelFactory<ConnectionSettings, ConnectionSettingsViewModel>>();
            Bind<WPF.ViewModels.Factories.IConnectionViewModelFactory>().To<ConnectionViewModelFactory>();
            Bind<WPF.ViewModels.Factories.IEditConnectionViewModelFactory<ConnectionSettings>>().To<EditConnectionViewModelFactory>();
        }
    }
}
