// <copyright file="BuildProvidersViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.Generic;
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildProvidersViewModel" /> class.
    /// </summary>
    public class BuildProvidersViewModel : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProvidersViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="providers">The providers.</param>
        public BuildProvidersViewModel(IEventAggregator eventAggregator, IEnumerable<BuildProviderViewModel> providers)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(providers).IsNotNull();

            _eventAggregator = eventAggregator;
            Providers = providers;
        }

        /// <summary>
        /// Gets the view name.
        /// </summary>
        /// <value>
        /// The view name.
        /// </value>
        public override string ViewName => "Add connection";

        /// <summary>
        /// Gets the providers.
        /// </summary>
        /// <value>
        /// The providers.
        /// </value>
        public IEnumerable<BuildProviderViewModel> Providers { get; }

        /// <summary>
        /// View the connections.
        /// </summary>
        public void ViewConnections()
        {
            var message = new NavigationMessage(typeof(ConnectionsViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
