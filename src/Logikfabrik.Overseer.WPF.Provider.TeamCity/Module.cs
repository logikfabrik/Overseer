// <copyright file="Module.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity
{
    using Ninject.Extensions.Factory;
    using Ninject.Extensions.Interception.Infrastructure.Language;
    using Ninject.Modules;
    using ViewModels;

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
            Bind<Api.IApiClient>().To<Api.ApiClient>().Intercept().With<Caching.CacheInterceptor>();

            Bind<IBuildProviderFactory>().To<BuildProviderFactory<ConnectionSettings, BuildProvider>>();

            Bind<WPF.ViewModels.IBuildProviderViewModel>().To<BuildProviderViewModel>();
            Bind<Settings.ConnectionSettings>().To<ConnectionSettings>();
            Bind<WPF.ViewModels.EditConnectionViewModel<ConnectionSettings>>().To<EditConnectionViewModel>();
            Bind<WPF.ViewModels.Factories.IConnectionSettingsViewModelFactory<ConnectionSettings, ConnectionSettingsViewModel>>().To<WPF.ViewModels.Factories.ConnectionSettingsViewModelFactory<ConnectionSettings, ConnectionSettingsViewModel>>();
            Bind<WPF.ViewModels.Factories.IConnectionViewModelFactory>().To<WPF.ViewModels.Factories.ConnectionViewModelFactory<ConnectionSettings, WPF.ViewModels.ConnectionViewModel<ConnectionSettings>>>();
            Bind<WPF.ViewModels.Factories.IEditConnectionViewModelFactory<ConnectionSettings>>().ToFactory();
        }
    }
}