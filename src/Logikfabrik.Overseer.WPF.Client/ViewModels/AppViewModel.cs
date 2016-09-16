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
    public sealed class AppViewModel : Conductor<PropertyChangedBase>, IHandle<NavigationEvent>
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

            ActivateItem(connectionsViewModel);
        }

        /// <summary>
        /// Handles the specified event message.
        /// </summary>
        /// <param name="eventMessage">The event message to handle.</param>
        public void Handle(NavigationEvent eventMessage)
        {
            var viewModel = IoC.GetInstance(eventMessage.ViewModelType, null) as PropertyChangedBase;

            ActivateItem(viewModel);
        }
    }
}
