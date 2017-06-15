// <copyright file="AppBootstrapper.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Markup;
    using Caliburn.Micro;
    using log4net.Config;
    using Logging;
    using Ninject;
    using Ninject.Parameters;
    using ViewModels;

    /// <summary>
    /// The <see cref="AppBootstrapper" /> class.
    /// </summary>
    public class AppBootstrapper : BootstrapperBase, IDisposable
    {
        private IKernel _kernel;
        private KernelHelper _kernelHelper;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppBootstrapper" /> class.
        /// </summary>
        public AppBootstrapper()
        {
            new AssemblyLoader().Load();

            _kernel = new StandardKernel();
            _kernelHelper = new KernelHelper();

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

            SetXamlBindingLanguage();

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
                _kernelHelper.ConfigureDesignTime(_kernel);
            }
            else
            {
                _kernelHelper.ConfigureRunTime(_kernel);

                LogManager.GetLog = type => _kernel.Get<IUILogService>(new ConstructorArgument("type", type));
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
            return Execute.InDesignMode ? base.SelectAssemblies() : _kernelHelper.Assemblies;
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

                _kernelHelper = null;
            }

            _isDisposed = true;
        }

        private void SetXamlBindingLanguage()
        {
            var language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.Name);

            FrameworkContentElement.LanguageProperty.OverrideMetadata(typeof(TextElement), new FrameworkPropertyMetadata(language));
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(language));
        }
    }
}