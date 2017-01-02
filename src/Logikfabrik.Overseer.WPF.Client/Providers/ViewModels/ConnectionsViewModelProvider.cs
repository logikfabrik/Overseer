// <copyright file="ConnectionsViewModelProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Providers.ViewModels
{
    using System;
    using System.Linq;
    using Caliburn.Micro;
    using Ninject;
    using Ninject.Activation;
    using Ninject.Parameters;
    using Overseer.Settings;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="ConnectionsViewModelProvider" /> class.
    /// </summary>
    public class ConnectionsViewModelProvider : Provider<ConnectionsViewModel>
    {
        /// <summary>
        /// Creates an instance within the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The created instance.
        /// </returns>
        protected override ConnectionsViewModel CreateInstance(IContext context)
        {
            Func<ConnectionSettings, ConnectionViewModel> getViewModel = settings =>
            {
                var moduleName = ModuleHelper.GetModuleName(settings);
                var parameters = new ConstructorArgument("settingsId", settings.Id);

                var viewModel = context.Kernel.Get<ConnectionViewModel>(moduleName, parameters);

                viewModel.SettingsName = settings.Name;

                return viewModel;
            };

            var repository = context.Kernel.Get<IConnectionSettingsRepository>();

            var viewModels = repository.Get().Select(getViewModel);

            return new ConnectionsViewModel(context.Kernel.Get<IEventAggregator>(), viewModels);
        }
    }
}
