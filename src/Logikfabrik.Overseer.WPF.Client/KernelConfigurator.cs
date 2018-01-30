// <copyright file="KernelConfigurator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Input;
    using Caliburn.Micro;
    using EnsureThat;
    using Logging;
    using Navigation.Factories;
    using Ninject;
    using Ninject.Extensions.Factory;
    using Overseer.Logging;
    using Providers;
    using Providers.Settings;
    using Serilog;
    using Settings;
    using ViewModels;
    using ViewModels.Factories;

    /// <summary>
    /// The <see cref="KernelConfigurator" /> class.
    /// </summary>
    public static class KernelConfigurator
    {
        /// <summary>
        /// Configures the specified kernel.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <param name="modules">The modules.</param>
        public static void Configure(IKernel kernel, IEnumerable<Assembly> modules)
        {
            Ensure.That(kernel).IsNotNull();
            Ensure.That(modules).IsNotNull();

            kernel.Bind<AppDomain>().ToConstant(AppDomain.CurrentDomain);
            kernel.Bind<IApp>().ToConstant((IApp)Application.Current);
            kernel.Bind<InputManager>().ToProvider<InputManagerProvider>();

            // Serilog setup.
            kernel.Bind<ILogger>().ToProvider<LoggerProvider>().InSingletonScope();

            // Business logic setup.
            kernel.Bind<IAppSettingsFactory>().ToFactory();
            kernel.Bind<ILogService>().To<LogService>();
            kernel.Bind<IConnectionSettingsSerializer>().ToProvider<ConnectionSettingsSerializerProvider>();
            kernel.Bind<IFileSystem>().To<FileSystem>();
            kernel.Bind<IFileStore>().ToProvider<FileStoreProvider>();
            kernel.Bind<IDataProtector>().To<DataProtector>();
            kernel.Bind<IRegistryStore>().ToProvider<RegistryStoreProvider>();
            kernel.Bind<IConnectionSettingsEncrypter>().To<ConnectionSettingsEncrypter>().InSingletonScope();
            kernel.Bind<IConnectionSettingsStore>().To<ConnectionSettingsStore>();
            kernel.Bind<IConnectionSettingsRepository>().To<ConnectionSettingsRepository>().InSingletonScope();
            kernel.Bind<IBuildProviderStrategy>().To<BuildProviderStrategy>();
            kernel.Bind<IConnectionPool>().To<ConnectionPool>().InSingletonScope();
            kernel.Bind<IBuildTracker>().To<BuildTracker>().InSingletonScope();

            // Caliburn Micro setup.
            kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
            kernel.Bind<IPlatformProvider>().ToConstant(PlatformProvider.Current);

            // App setup.
            kernel.Bind<INavigationMessageFactory<EditSettingsViewModel>>().ToFactory();
            kernel.Bind<INavigationMessageFactory<NewConnectionViewModel>>().ToFactory();
            kernel.Bind<INavigationMessageFactory<RemoveConnectionViewModel>>().ToFactory();
            kernel.Bind<INavigationMessageFactory<ViewDashboardViewModel>>().ToFactory();
            kernel.Bind<INavigationMessageFactory<ViewConnectionsViewModel>>().ToFactory();
            kernel.Bind<INavigationMessageFactory<ViewProjectViewModel>>().ToFactory();
            kernel.Bind<INavigationMessageFactory<ViewAboutViewModel>>().ToFactory();
            kernel.Bind<INavigationMessageFactory<WizardEditPassphraseViewModel>>().ToFactory();
            kernel.Bind<INavigationMessageFactory<WizardFinishViewModel>>().ToFactory();
            kernel.Bind<INavigationMessageFactory<WizardNewConnectionViewModel>>().ToFactory();

            kernel.Bind<IUILogService>().To<UILogService>();
            kernel.Bind<IDisplaySetting>().To<DisplaySetting>();

            kernel.Bind<IViewNotificationViewModelFactory>().ToFactory();
            kernel.Bind<IBuildNotificationManager>().To<BuildNotificationManager>().InSingletonScope();
            kernel.Bind<IEditTrackedProjectViewModelFactory>().ToFactory();
            kernel.Bind<IEditTrackedProjectsViewModelFactory>().ToFactory();
            kernel.Bind<IViewChangeViewModelFactory>().ToFactory();
            kernel.Bind<IViewBuildViewModelFactory>().ToFactory();
            kernel.Bind<IViewProjectViewModelFactory>().ToFactory();
            kernel.Bind<IRemoveConnectionViewModelFactory>().ToFactory();
            kernel.Bind<IViewConnectionViewModelStrategy>().To<ViewConnectionViewModelStrategy>();
            kernel.Bind<ConnectionsViewModel>().ToSelf().InSingletonScope();

            // WPF client setup.
            kernel.Bind<IMouseManager>().To<MouseManager>();

            kernel.Load(modules);
        }
    }
}