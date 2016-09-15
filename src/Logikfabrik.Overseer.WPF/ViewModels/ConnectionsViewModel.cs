// <copyright file="ConnectionsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionsViewModel" /> class.
    /// </summary>
    public class ConnectionsViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IBuildMonitor _buildMonitor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionsViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildProviderRepository">The build provider repository.</param>
        /// <param name="buildMonitor">The build monitor.</param>
        public ConnectionsViewModel(IEventAggregator eventAggregator, IBuildProviderRepository buildProviderRepository, IBuildMonitor buildMonitor)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildProviderRepository).IsNotNull();
            Ensure.That(buildMonitor).IsNotNull();

            _eventAggregator = eventAggregator;
            ConnectionViewModels = buildProviderRepository.GetProviders().Select(provider => new ConnectionViewModel(provider));
            _buildMonitor = buildMonitor;

            _buildMonitor.ProgressChanged += ProgressChanged;
            _buildMonitor.StartMonitoring();
        }

        private void ProgressChanged(object sender, BuildMonitorProgressEventArgs e)
        {
            Debug.WriteLine($"Progress at {DateTime.Now}: {e.PercentProgress}%");
        }

        /// <summary>
        /// Gets the connections
        /// </summary>
        /// <value>
        /// The connections.
        /// </value>
        public IEnumerable<ConnectionViewModel> ConnectionViewModels { get; } // TODO: Projects/builds for each connection might be added/removed externally. Should this be observable?

        public void AddConnection()
        {
            var eventMessage = new NavigationEvent(typeof(BuildProvidersViewModel));

            _eventAggregator.PublishOnUIThread(eventMessage);
        }
    }
}
