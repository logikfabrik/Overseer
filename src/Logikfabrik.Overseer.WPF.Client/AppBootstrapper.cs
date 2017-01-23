// <copyright file="AppBootstrapper.cs" company="Logikfabrik">
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
    using Caliburn.Micro;
    using log4net.Config;
    using Logging;
    using Ninject;
    using Ninject.Modules;
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
        private readonly IKernel _kernel;
        private readonly Lazy<IEnumerable<Assembly>> _assemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppBootstrapper" /> class.
        /// </summary>
        public AppBootstrapper()
        {
            _kernel = new StandardKernel();
            _assemblies = new Lazy<IEnumerable<Assembly>>(GetAssemblies);

            Initialize();

            XmlConfigurator.Configure();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Called on app start.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            base.OnStartup(sender, e);

            DisplayRootViewFor<AppViewModel>();
        }

        /// <summary>
        /// Called on app end.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        protected override void OnExit(object sender, EventArgs e)
        {
            base.OnExit(sender, e);

            if (!_kernel.IsDisposed)
            {
                _kernel.Dispose();
            }
        }

        /// <summary>
        /// Configure the container.
        /// </summary>
        protected override void Configure()
        {
            base.Configure();

            // TODO: Discover and add automatically.
            ViewLocator.AddNamespaceMapping("*", "Logikfabrik.Overseer.WPF.Client.Views");

            // Business logic setup.
            _kernel.Bind<ILogService>().To<LogService>();
            _kernel.Bind<IConnectionSettingsSerializer>().ToProvider<ConnectionSettingsSerializerProvider>().InSingletonScope();
            _kernel.Bind<IFileStore>().ToProvider<FileStoreProvider>().InSingletonScope();
            _kernel.Bind<IConnectionSettingsStore>().To<ConnectionSettingsStore>();
            _kernel.Bind<IConnectionSettingsRepository>().To<ConnectionSettingsRepository>().InSingletonScope();
            _kernel.Bind<IConnectionPool>().To<ConnectionPool>().InSingletonScope();
            _kernel.Bind<IBuildMonitor>().To<BuildMonitor>().InSingletonScope();

            // Caliburn Micro setup.
            _kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            _kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();

            // WPF client setup.
            _kernel.Bind<INotificationManager>().To<NotificationManager>();
            _kernel.Bind<IBuildNotificationManager>().To<BuildNotificationManager>().InSingletonScope();
            _kernel.Bind<IProjectToMonitorViewModelFactory>().To<ProjectToMonitorViewModelFactory>();
            _kernel.Bind<IChangeViewModelFactory>().To<ChangeViewModelFactory>();
            _kernel.Bind<IBuildViewModelFactory>().To<BuildViewModelFactory>();
            _kernel.Bind<IProjectViewModelFactory>().To<ProjectViewModelFactory>();
            _kernel.Bind<IRemoveConnectionViewModelFactory>().To<RemoveConnectionViewModelFactory>();
            _kernel.Bind<IConnectionViewModelStrategy>().To<ConnectionViewModelStrategy>();
            _kernel.Bind<ConnectionsViewModel>().ToSelf().InSingletonScope();

            _kernel.Load(SelectAssemblies());
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
            return _assemblies.Value;
        }

        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // ReSharper disable once InvertIf
            if (disposing)
            {
                if (_kernel == null || _kernel.IsDisposed)
                {
                    return;
                }

                _kernel.Dispose();
            }
        }

        private static void LoadAllAssemblies()
        {
            var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (string.IsNullOrWhiteSpace(directoryName))
            {
                return;
            }

            var assemblyPaths = Directory.EnumerateFiles(directoryName, "*.dll", SearchOption.TopDirectoryOnly);

            var assembliesInAppDomain = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var path in assemblyPaths)
            {
                var assemblyName = AssemblyName.GetAssemblyName(path);

                if (assembliesInAppDomain.Any(assembly => AssemblyName.ReferenceMatchesDefinition(assemblyName, assembly.GetName())))
                {
                    continue;
                }

                Assembly.Load(assemblyName);
            }
        }

        private IEnumerable<Assembly> GetAssemblies()
        {
            LoadAllAssemblies();

            var assemblies = new List<Assembly>(base.SelectAssemblies());

            assemblies.AddRange(
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(assembly => !assembly.IsDynamic && assembly.GetExportedTypes()
                        .Any(type => !type.IsAbstract && typeof(NinjectModule).IsAssignableFrom(type))));

            return assemblies;
        }
    }
}
