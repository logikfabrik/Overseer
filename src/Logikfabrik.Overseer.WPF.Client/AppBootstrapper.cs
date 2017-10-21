// <copyright file="AppBootstrapper.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;
    using Caliburn.Micro;
    using Logging;
    using Ninject;
    using Ninject.Parameters;
    using Overseer.Logging;
    using ViewModels;

    /// <summary>
    /// The <see cref="AppBootstrapper" /> class.
    /// </summary>
    public class AppBootstrapper : BootstrapperBase, IDisposable
    {
        private IKernel _kernel;
        private AppCatalog _catalog;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppBootstrapper" /> class.
        /// </summary>
        public AppBootstrapper()
        {
            _kernel = new StandardKernel();
            _catalog = new AppCatalog(AppCatalog.GetProduct(GetType().Assembly));

            Initialize();
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
                _kernel.Get<IApp>().Shutdown();

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

            if (!Execute.InDesignMode)
            {
                KernelConfigurator.Configure(_kernel, _catalog.Modules);

#pragma warning disable S2696
                LogManager.GetLog = type => _kernel.Get<IUILogService>(new ConstructorArgument("type", type));
#pragma warning restore S2696
            }

            ViewLocator.AddNamespaceMapping("*", "Logikfabrik.Overseer.WPF.Client.Views");

            _catalog = null;

            LanguageConfigurator.Configure(_kernel.Get<IAppSettingsFactory>());
            DataBindingLanguageConfigurator.Configure();
            DataBindingActionConfigurator.Configure();
            ConventionConfigurator.Configure();
            ErrorLogHandlerConfigurator.Configure(_kernel.Get<AppDomain>(), _kernel.Get<IApp>(), _kernel.Get<ILogService>());
            BuildNotificationConfigurator.Configure(_kernel.Get<IBuildMonitor>(), _kernel.Get<IBuildNotificationManager>());
        }

        /// <summary>
        /// Gets the instance of the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="key">The key.</param>
        /// <returns>The instance of the specified service.</returns>
        protected override object GetInstance(Type service, string key)
        {
            return string.IsNullOrEmpty(key) ? _kernel.Get(service) : _kernel.Get(service, key);
        }

        /// <summary>
        /// Gets all instances of the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns>All instances of the specified service type.</returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _kernel.GetAll(service);
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
            return Execute.InDesignMode ? base.SelectAssemblies() : _catalog.Assemblies;
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

                _catalog = null;
            }

            _isDisposed = true;
        }
    }
}