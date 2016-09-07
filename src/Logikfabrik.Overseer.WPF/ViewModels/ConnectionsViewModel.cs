// <copyright file="ConnectionsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Caliburn.Micro;
    using Settings;

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
        /// <param name="buildProviderSettingsRepository">The build provider settings repository.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="eventAggregator" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="buildProviderSettingsRepository" /> is <c>null</c>.</exception>
        public ConnectionsViewModel(IEventAggregator eventAggregator, IBuildProviderSettingsRepository buildProviderSettingsRepository)
        {
            if (eventAggregator == null)
            {
                throw new ArgumentNullException(nameof(eventAggregator));
            }

            if (buildProviderSettingsRepository == null)
            {
                throw new ArgumentNullException(nameof(buildProviderSettingsRepository));
            }

            _eventAggregator = eventAggregator;
            ConnectionViewModels = buildProviderSettingsRepository.Get().Select(settings => new ConnectionViewModel(settings));
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
