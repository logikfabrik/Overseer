// <copyright file="AppViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using System;
    using Caliburn.Micro;
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
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="eventAggregator" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="connectionsViewModel" /> is <c>null</c>.</exception>
        public AppViewModel(IEventAggregator eventAggregator, ConnectionsViewModel connectionsViewModel)
        {
            if (eventAggregator == null)
            {
                throw new ArgumentNullException(nameof(eventAggregator));
            }

            if (connectionsViewModel == null)
            {
                throw new ArgumentNullException(nameof(connectionsViewModel));
            }

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
