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
        /// <param name="connectionViewModels">The connection view models.</param>
        public ConnectionsViewModel(IEventAggregator eventAggregator, IEnumerable<ConnectionViewModel> connectionViewModels)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(connectionViewModels).IsNotNull();

            _eventAggregator = eventAggregator;
            ConnectionViewModels = connectionViewModels;
        }

        /// <summary>
        /// Gets the view name.
        /// </summary>
        /// <value>
        /// The view name.
        /// </value>
        public override string ViewName => "Connections";

        /// <summary>
        /// Gets the connection view models.
        /// </summary>
        /// <value>
        /// The connection view models.
        /// </value>
        public IEnumerable<ConnectionViewModel> ConnectionViewModels { get; }

        public void AddConnection()
        {
            var message = new NavigationMessage(typeof(BuildProvidersViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
