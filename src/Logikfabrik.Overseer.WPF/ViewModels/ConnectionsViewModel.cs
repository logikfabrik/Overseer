// <copyright file="ConnectionsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Caliburn.Micro;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="ConnectionsViewModel" /> class.
    /// </summary>
    public class ConnectionsViewModel : ViewModel, IObserver<ConnectionSettings[]>
    {
        private readonly IDisposable _subscription;
        private readonly IEventAggregator _eventAggregator;
        private readonly List<ConnectionViewModel> _connections;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionsViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="settingsRepository">The settings repository.</param>
        public ConnectionsViewModel(IEventAggregator eventAggregator, IConnectionSettingsRepository settingsRepository)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(settingsRepository).IsNotNull();

            _eventAggregator = eventAggregator;
            _connections = new List<ConnectionViewModel>();
            _subscription = settingsRepository.Subscribe(this);
        }

        /// <summary>
        /// Gets the view name.
        /// </summary>
        /// <value>
        /// The view name.
        /// </value>
        public override string ViewName { get; } = "Connections";

        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <value>
        /// The connections.
        /// </value>
        public IEnumerable<ConnectionViewModel> Connections => _connections;

        /// <summary>
        /// Add a connection.
        /// </summary>
        public void AddConnection()
        {
            var message = new NavigationMessage(typeof(BuildProvidersViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }

        /// <summary>
        /// Provides the observer with new data.
        /// </summary>
        /// <param name="value">The current notification information.</param>
        public void OnNext(ConnectionSettings[] value)
        {
            var isDirty = false;

            foreach (var settings in value)
            {
                var connectionToUpdate = _connections.SingleOrDefault(c => c.SettingsId == settings.Id);

                if (connectionToUpdate != null)
                {
                    // TODO: Update the view model.
                }
                else
                {
                    // TODO: Create and add view model.
                    //var connectionToAdd = IoC.

                    //_connections.Add(connectionToAdd);

                    isDirty = true;
                }
            }

            var connectionsToKeep = value.Select(settings => settings.Id).ToArray();

            isDirty = isDirty || _connections.RemoveAll(viewModel => !connectionsToKeep.Contains(viewModel.SettingsId)) == 0;

            if (isDirty)
            {
                NotifyOfPropertyChange(() => Connections);
            }
        }

        /// <summary>
        /// Notifies the observer that the provider has experienced an error condition.
        /// </summary>
        /// <param name="error">An object that provides additional information about the error.</param>
        public void OnError(Exception error)
        {
            // Do nothing, even if disposed (pattern practice).
        }

        /// <summary>
        /// Notifies the observer that the provider has finished sending push-based notifications.
        /// </summary>
        public void OnCompleted()
        {
            // Do nothing, even if disposed (pattern practice).
        }
    }
}
