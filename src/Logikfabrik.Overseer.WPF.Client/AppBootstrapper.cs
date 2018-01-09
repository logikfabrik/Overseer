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
    // ReSharper disable once InheritdocConsiderUsage
    public class AppBootstrapper : BootstrapperBase, IDisposable
    {
        private IKernel _kernel;
        private AppCatalog _catalog;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppBootstrapper" /> class.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public AppBootstrapper()
        {
            _kernel = new StandardKernel();
            _catalog = new AppCatalog(AppDomain.CurrentDomain, AppCatalog.GetProduct(GetType().Assembly));

            Initialize();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            base.OnStartup(sender, e);

            bool? dialogResult = true;

            var viewModel = _kernel.Get<StartWizardViewModel>();

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

        /// <inheritdoc />
        protected override void Configure()
        {
            base.Configure();

            if (!Execute.InDesignMode)
            {
                KernelConfigurator.Configure(_kernel, _catalog.Modules);

#pragma warning disable S2696 // Instance members should not write to "static" fields
                LogManager.GetLog = type => _kernel.Get<IUILogService>(new ConstructorArgument("type", type));
#pragma warning restore S2696 // Instance members should not write to "static" fields
            }

            ViewLocator.AddNamespaceMapping("*", "Logikfabrik.Overseer.WPF.Client.Views");

            _catalog = null;

            LanguageConfigurator.Configure(_kernel.Get<IAppSettingsFactory>());
            DataBindingLanguageConfigurator.Configure();
            DataBindingActionConfigurator.Configure();
            ConventionConfigurator.Configure();
            ErrorLogHandlerConfigurator.Configure(_kernel.Get<AppDomain>(), _kernel.Get<IApp>(), _kernel.Get<ILogService>());
            BuildNotificationConfigurator.Configure(_kernel.Get<IBuildTracker>(), _kernel.Get<IBuildNotificationManager>());
        }

        /// <inheritdoc />
        protected override object GetInstance(Type service, string key)
        {
            return string.IsNullOrEmpty(key) ? _kernel.Get(service) : _kernel.Get(service, key);
        }

        /// <inheritdoc />
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _kernel.GetAll(service);
        }

        /// <inheritdoc />
        protected override void BuildUp(object instance)
        {
            _kernel.Inject(instance);
        }

        /// <inheritdoc />
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