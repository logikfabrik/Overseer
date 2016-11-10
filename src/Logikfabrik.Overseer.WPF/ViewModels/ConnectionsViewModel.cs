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
    public class ConnectionsViewModel : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionsViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildMonitor">The build monitor.</param>
        /// <param name="buildProviderRepository">The build provider repository.</param>
        public ConnectionsViewModel(IEventAggregator eventAggregator, IBuildMonitor buildMonitor, IBuildProviderRepository buildProviderRepository)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildProviderRepository).IsNotNull();
            Ensure.That(buildMonitor).IsNotNull();

            _eventAggregator = eventAggregator;
            ConnectionViewModels = buildProviderRepository.GetBuildProviders().Select(buildProvider => new ConnectionViewModel(buildMonitor, buildProvider));

            buildMonitor.StartMonitoring();
        }

        /// <summary>
        /// Gets the view name.
        /// </summary>
        /// <value>
        /// The view name.
        /// </value>
        public override string ViewName => "Connections";

        /// <summary>
        /// Gets the connections
        /// </summary>
        /// <value>
        /// The connections.
        /// </value>
        public IEnumerable<ConnectionViewModel> ConnectionViewModels { get; }

        public void AddConnection()
        {
            var message = new NavigationMessage(typeof(BuildProvidersViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
