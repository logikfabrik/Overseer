// <copyright file="ConnectionsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.Generic;
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
        /// <param name="connections">The connections.</param>
        public ConnectionsViewModel(IEventAggregator eventAggregator, IEnumerable<ConnectionViewModel> connections)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(connections).IsNotNull();

            _eventAggregator = eventAggregator;
            Connections = connections;
        }

        /// <summary>
        /// Gets the view name.
        /// </summary>
        /// <value>
        /// The view name.
        /// </value>
        public override string ViewName => "Connections";

        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <value>
        /// The connections.
        /// </value>
        public IEnumerable<ConnectionViewModel> Connections { get; }

        /// <summary>
        /// Add a connection.
        /// </summary>
        public void AddConnection()
        {
            var message = new NavigationMessage(typeof(BuildProvidersViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
