﻿// <copyright file="AppBootstrapper.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using CacheManager.Core;
    using Caliburn.Micro;
    using log4net.Config;
    using Logging;
    using Ninject;
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using Ninject.Parameters;
    using Overseer.Logging;
    using Providers.Caching;
    using Providers.Settings;
    using Settings;
    using ViewModels;
    using WPF.ViewModels;
    using WPF.ViewModels.Factories;

    /// <summary>
    /// The <see cref="AppBootstrapper" /> class.
    /// </summary>
    public class AppBootstrapper : BootstrapperBase, IDisposable
    {
        private IKernel _kernel;
        private Lazy<IEnumerable<Assembly>> _runtimeAssemblies;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppBootstrapper" /> class.
        /// </summary>
        public AppBootstrapper()
        {
            _kernel = new StandardKernel();
            _runtimeAssemblies = new Lazy<IEnumerable<Assembly>>(GetRuntimeAssemblies);

            XmlConfigurator.Configure();

            Initialize();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Called on app start.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            base.OnStartup(sender, e);

            bool? dialogResult = true;

            var viewModel = _kernel.Get<PassPhraseViewModel>();

            viewModel.CanClose(canClose =>
            {
                if (!canClose)
                {
                    dialogResult = _kernel.Get<IWindowManager>().ShowDialog(viewModel);
                }
            });

            if (dialogResult == null || !dialogResult.Value)
            {
                Application.Current.Shutdown();

                return;
            }

            DisplayRootViewFor<AppViewModel>();
        }

        /// <summary>
        /// Configure the container.
        /// </summary>
        protected override void Configure()
        {
            base.Configure();

            if (Execute.InDesignMode)
            {
                ConfigureDesignTime();
            }
            else
            {
                ConfigureRuntime();
            }

            ViewLocator.AddNamespaceMapping("*", "Logikfabrik.Overseer.WPF.Client.Views");
        }

        /// <summary>
        /// Gets the instance of the specified service type.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <param name="key">The key.</param>
        /// <returns>The instance of the specified service type.</returns>
        protected override object GetInstance(Type serviceType, string key)
        {
            return string.IsNullOrEmpty(key) ? _kernel.Get(serviceType) : _kernel.Get(serviceType, key);
        }

        /// <summary>
        /// Gets all instances of the specified service type.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <returns>All instances of the specified service type.</returns>
        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        /// <summary>
        /// Builds the specified instance up.
        /// </summary>
        /// <param name="instance">The instance to build up.</param>
        protected override void BuildUp(object instance)
        {
            _kernel.Inject(instance);
        }

        /// <summary>
        /// Select assemblies to load and to inspect for views.
        /// </summary>
        /// <returns>
        /// Assemblies to load and to inspect.
        /// </returns>
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return Execute.InDesignMode ? base.SelectAssemblies() : _runtimeAssemblies.Value;
        }

        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (_kernel != null)
                {
                    _kernel.Dispose();
                    _kernel = null;
                }

                _runtimeAssemblies = null;
            }

            _isDisposed = true;
        }

        private IEnumerable<Assembly> GetRuntimeAssemblies()
        {
            LoadAllAssemblies();

            var product = GetType().Assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product;

            Func<Assembly, bool> isProductAssembly = assembly => assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product == product;

            return AppDomain.CurrentDomain.GetAssemblies().Where(assembly => !assembly.IsDynamic && isProductAssembly(assembly)).ToArray();
        }

        private IEnumerable<Assembly> GetModules()
        {
            return _runtimeAssemblies.Value.Where(assembly => assembly.GetExportedTypes().Any(type => !type.IsAbstract && typeof(INinjectModule).IsAssignableFrom(type)));
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private void ConfigureDesignTime()
        {
            // Do nothing.
        }

        private void ConfigureRuntime()
        {
            // Business logic setup.
            _kernel.Bind<IAppSettingsFactory>().ToFactory();
            _kernel.Bind<ICacheManager<object>>().ToProvider<CacheManagerProvider>();
            _kernel.Bind<ILogService>().To<LogService>();
            _kernel.Bind<IConnectionSettingsSerializer>().ToProvider<ConnectionSettingsSerializerProvider>();
            _kernel.Bind<IFileStore>().ToProvider<FileStoreProvider>();
            _kernel.Bind<IDataProtector>().To<DataProtector>();
            _kernel.Bind<IRegistryStore>().ToProvider<RegistryStoreProvider>();
            _kernel.Bind<IConnectionSettingsEncrypter>().To<ConnectionSettingsEncrypter>().InSingletonScope();
            _kernel.Bind<IConnectionSettingsStore>().To<ConnectionSettingsStore>();
            _kernel.Bind<IConnectionSettingsRepository>().To<ConnectionSettingsRepository>().InSingletonScope();
            _kernel.Bind<IBuildProviderStrategy>().To<BuildProviderStrategy>();
            _kernel.Bind<IConnectionPool>().To<ConnectionPool>().InSingletonScope();
            _kernel.Bind<IBuildMonitor>().To<BuildMonitor>().InSingletonScope();

            // Caliburn Micro setup.
            _kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            _kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();

            // WPF client setup.
            _kernel.Bind<IUILogService>().To<UILogService>();
            _kernel.Bind<INotificationManager>().To<NotificationManager>();
            _kernel.Bind<IBuildNotificationManager>().To<BuildNotificationManager>().InSingletonScope();
            _kernel.Bind<IProjectToMonitorViewModelFactory>().ToFactory();
            _kernel.Bind<IProjectsToMonitorViewModelFactory>().ToFactory();
            _kernel.Bind<IChangeViewModelFactory>().ToFactory();
            _kernel.Bind<IBuildViewModelFactory>().ToFactory();
            _kernel.Bind<IProjectViewModelFactory>().ToFactory();
            _kernel.Bind<IRemoveConnectionViewModelFactory>().ToFactory();
            _kernel.Bind<IConnectionViewModelStrategy>().To<ConnectionViewModelStrategy>();
            _kernel.Bind<ConnectionsViewModel>().ToSelf().InSingletonScope();

            _kernel.Load(GetModules());

            LogManager.GetLog = type => _kernel.Get<IUILogService>(new ConstructorArgument("type", type));
        }

        private void LoadAllAssemblies()
        {
            var directoryName = Path.GetDirectoryName(GetType().Assembly.Location);

            if (string.IsNullOrWhiteSpace(directoryName))
            {
                return;
            }

            var assemblyPaths = Directory.EnumerateFiles(directoryName, "*.dll", SearchOption.TopDirectoryOnly);

            foreach (var path in assemblyPaths)
            {
                AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path));
            }
        }
    }
}