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
    public class BuildProvidersViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProvidersViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildProviderViewModels">The build provider view models.</param>
        public BuildProvidersViewModel(IEventAggregator eventAggregator, IEnumerable<BuildProviderViewModel> buildProviderViewModels)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildProviderViewModels).IsNotNull();

            _eventAggregator = eventAggregator;
            BuildProviderViewModels = buildProviderViewModels;
        }

        /// <summary>
        /// Gets the build providers.
        /// </summary>
        /// <value>
        /// The build providers.
        /// </value>
        public IEnumerable<BuildProviderViewModel> BuildProviderViewModels { get; }

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
