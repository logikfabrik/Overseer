// <copyright file="Module.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity
{
    using Caliburn.Micro;
    using Navigation;
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

            Bind<INavigationMessageFactory<WPF.ViewModels.EditConnectionViewModel<ConnectionSettings>>>().ToFactory();
            Bind<INavigationMessageFactory<WPF.ViewModels.ViewConnectionViewModel<ConnectionSettings>>>().ToFactory();
            Bind<INavigationMessageFactory<WPF.ViewModels.AddConnectionViewModel<ConnectionSettings, EditConnectionSettingsViewModel>>>().ToFactory();

            Bind<WPF.ViewModels.IBuildProviderViewModel>().To<BuildProviderViewModel>();
            Bind<WPF.ViewModels.EditConnectionViewModel<ConnectionSettings>>().To<EditConnectionViewModel>();
            Bind<WPF.ViewModels.Factories.IEditConnectionSettingsViewModelFactory<ConnectionSettings, EditConnectionSettingsViewModel>>().To<WPF.ViewModels.Factories.EditConnectionSettingsViewModelFactory<ConnectionSettings, EditConnectionSettingsViewModel>>();
            Bind<WPF.ViewModels.Factories.IViewConnectionViewModelFactory>().To<WPF.ViewModels.Factories.ViewConnectionViewModelFactory<ConnectionSettings, WPF.ViewModels.ViewConnectionViewModel<ConnectionSettings>>>();
            Bind<WPF.ViewModels.Factories.IEditConnectionViewModelFactory<ConnectionSettings>>().ToFactory();

            ViewLocator.AddNamespaceMapping("Logikfabrik.Overseer.WPF.Provider.TeamCity.ViewModels", "Logikfabrik.Overseer.WPF.Provider.TeamCity.Views.Views");
        }
    }
}