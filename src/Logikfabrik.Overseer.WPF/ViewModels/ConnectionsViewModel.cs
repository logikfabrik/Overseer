// <copyright file="ConnectionsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionsViewModel" /> class.
    /// </summary>
    public class ConnectionsViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionsViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildProviderRepository">The build provider repository.</param>
        public ConnectionsViewModel(IEventAggregator eventAggregator, IBuildProviderRepository buildProviderRepository)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildProviderRepository).IsNotNull();

            _eventAggregator = eventAggregator;
            ConnectionViewModels = buildProviderRepository.GetProviders().Select(provider => new ConnectionViewModel(provider));
        }

        /// <summary>
        /// Gets the connections
        /// </summary>
        /// <value>
        /// The connections.
        /// </value>
        public IEnumerable<ConnectionViewModel> ConnectionViewModels { get; }

        public void AddConnection()
        {
            var eventMessage = new NavigationEvent(typeof(BuildProvidersViewModel));

            _eventAggregator.PublishOnUIThread(eventMessage);
        }
    }
}
