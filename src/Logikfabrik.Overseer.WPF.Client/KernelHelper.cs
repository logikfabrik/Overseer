// <copyright file="KernelHelper.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using CacheManager.Core;
    using Caliburn.Micro;
    using EnsureThat;
    using Logging;
    using Ninject;
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using Overseer.Logging;
    using Providers.Caching;
    using Providers.Settings;
    using Settings;
    using WPF.ViewModels;
    using WPF.ViewModels.Factories;

    public class KernelHelper
    {
        private readonly Lazy<IEnumerable<Assembly>> _runtimeAssemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="KernelHelper" /> class.
        /// </summary>
        public KernelHelper()
        {
            _runtimeAssemblies = new Lazy<IEnumerable<Assembly>>(GetProductAssemblies);
        }

        public IEnumerable<Assembly> Assemblies => _runtimeAssemblies.Value;

        public void ConfigureDesignTime(IKernel kernel)
        {
            // Do nothing.
        }

        public void ConfigureRunTime(IKernel kernel)
        {
            Ensure.That(kernel).IsNotNull();

            // Business logic setup.
            kernel.Bind<IAppSettingsFactory>().ToFactory();
            kernel.Bind<ICacheManager<object>>().ToProvider<CacheManagerProvider>();
            kernel.Bind<ILogService>().To<LogService>();
            kernel.Bind<IConnectionSettingsSerializer>().ToProvider<ConnectionSettingsSerializerProvider>();
            kernel.Bind<IFileStore>().ToProvider<FileStoreProvider>();
            kernel.Bind<IDataProtector>().To<DataProtector>();
            kernel.Bind<IRegistryStore>().ToProvider<RegistryStoreProvider>();
            kernel.Bind<IConnectionSettingsEncrypter>().To<ConnectionSettingsEncrypter>().InSingletonScope();
            kernel.Bind<IConnectionSettingsStore>().To<ConnectionSettingsStore>();
            kernel.Bind<IConnectionSettingsRepository>().To<ConnectionSettingsRepository>().InSingletonScope();
            kernel.Bind<IBuildProviderStrategy>().To<BuildProviderStrategy>();
            kernel.Bind<IConnectionPool>().To<ConnectionPool>().InSingletonScope();
            kernel.Bind<IBuildMonitor>().To<BuildMonitor>().InSingletonScope();

            // Caliburn Micro setup.
            kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();

            // WPF client setup.
            kernel.Bind<IUILogService>().To<UILogService>();
            kernel.Bind<INotificationManager>().To<NotificationManager>();
            kernel.Bind<IBuildNotificationManager>().To<BuildNotificationManager>().InSingletonScope();
            kernel.Bind<IProjectToMonitorViewModelFactory>().ToFactory();
            kernel.Bind<IProjectsToMonitorViewModelFactory>().ToFactory();
            kernel.Bind<IChangeViewModelFactory>().ToFactory();
            kernel.Bind<IBuildViewModelFactory>().ToFactory();
            kernel.Bind<IProjectViewModelFactory>().ToFactory();
            kernel.Bind<IRemoveConnectionViewModelFactory>().ToFactory();
            kernel.Bind<IConnectionViewModelStrategy>().To<ConnectionViewModelStrategy>();
            kernel.Bind<ConnectionsViewModel>().ToSelf().InSingletonScope();

            kernel.Load(GetModules());
        }

        private IEnumerable<Assembly> GetProductAssemblies()
        {
            var product = GetType().Assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product;

            Func<Assembly, bool> isProductAssembly = assembly => assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product == product;

            return AppDomain.CurrentDomain.GetAssemblies().Where(assembly => !assembly.IsDynamic && isProductAssembly(assembly)).ToArray();
        }

        private IEnumerable<Assembly> GetModules()
        {
            return _runtimeAssemblies.Value.Where(assembly => assembly.GetExportedTypes().Any(type => !type.IsAbstract && typeof(INinjectModule).IsAssignableFrom(type)));
        }
    }
}
