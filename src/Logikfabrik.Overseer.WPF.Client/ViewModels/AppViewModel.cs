// <copyright file="AppViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="AppViewModel" /> class.
    /// </summary>
    public sealed class AppViewModel : Conductor<PropertyChangedBase>, IHandle<NavigationMessage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="connectionsViewModel">The connections view model.</param>
        public AppViewModel(IEventAggregator eventAggregator, ConnectionsViewModel connectionsViewModel)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(connectionsViewModel).IsNotNull();

            eventAggregator.Subscribe(this);

            DisplayName = "Overseer";

            ActivateItem(connectionsViewModel);
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message to handle.</param>
        public void Handle(NavigationMessage message)
        {
            var viewModel = IoC.GetInstance(message.ViewModelType, null) as PropertyChangedBase;

            ActivateItem(viewModel);
        }
    }
}
