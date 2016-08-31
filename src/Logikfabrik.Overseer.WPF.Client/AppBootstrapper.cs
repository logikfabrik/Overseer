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
    using Ninject;
    using Ninject.Modules;
    using Settings;
    using ViewModels;

    /// <summary>
    /// The <see cref="AppBootstrapper" /> class.
    /// </summary>
    public class AppBootstrapper : BootstrapperBase
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

            // Default setup.
            _kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            _kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();

            // Program setup.
            _kernel.Bind<IBuildProviderSettingsStore>().To<BuildProviderSettingsStore>().InSingletonScope();
            _kernel.Bind<IBuildProviderSettingsRepository>().To<BuildProviderSettingsRepository>().InSingletonScope();

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
            var assemblies = _assemblies.Value;

            return assemblies;
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
                    .Where(assembly => !assembly.IsDynamic)
                    .Where(assembly => assembly.GetExportedTypes().Any(type => typeof(NinjectModule).IsAssignableFrom(type) && !type.IsAbstract)));

            return assemblies;
        }
    }
}
