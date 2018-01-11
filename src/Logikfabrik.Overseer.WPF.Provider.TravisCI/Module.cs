// <copyright file="Module.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI
{
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using ViewModels;

    /// <summary>
    /// The <see cref="Module" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class Module : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
            Bind<Api.IApiClient>().To<Api.ApiClient>();

            Bind<IBuildProviderFactory>().To<BuildProviderFactory<ConnectionSettings, BuildProvider>>();

            Bind<WPF.ViewModels.IBuildProviderViewModel>().To<BuildProviderViewModel>();
            Bind<WPF.ViewModels.EditConnectionViewModel<ConnectionSettings>>().To<EditConnectionViewModel>();
            Bind<WPF.ViewModels.Factories.IConnectionSettingsViewModelFactory<ConnectionSettings, ConnectionSettingsViewModel>>().To<WPF.ViewModels.Factories.ConnectionSettingsViewModelFactory<ConnectionSettings, ConnectionSettingsViewModel>>();
            Bind<WPF.ViewModels.Factories.IConnectionViewModelFactory>().To<WPF.ViewModels.Factories.ConnectionViewModelFactory<ConnectionSettings, WPF.ViewModels.ViewConnectionViewModel<ConnectionSettings>>>();
            Bind<WPF.ViewModels.Factories.IEditConnectionViewModelFactory<ConnectionSettings>>().ToFactory();
        }
    }
}