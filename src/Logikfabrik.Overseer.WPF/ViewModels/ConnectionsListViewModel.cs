// <copyright file="ConnectionsListViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Caliburn.Micro;
    using EnsureThat;
    using Factories;
    using Settings;

    /// <summary>
    /// The <see cref="ConnectionsListViewModel" /> class.
    /// </summary>
    public class ConnectionsListViewModel : PropertyChangedBase, IObserver<Notification<ConnectionSettings>[]>, IDisposable
    {
        private readonly IConnectionViewModelStrategy _connectionViewModelStrategy;
        private List<IConnectionViewModel> _connections;
        private IDisposable _subscription;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionsListViewModel" /> class.
        /// </summary>
        /// <param name="connectionViewModelStrategy">The connection view model strategy.</param>
        /// <param name="connectionSettingsRepository">The connection settings repository.</param>
        public ConnectionsListViewModel(
            IConnectionViewModelStrategy connectionViewModelStrategy,
            IConnectionSettingsRepository connectionSettingsRepository)
        {
            Ensure.That(connectionViewModelStrategy).IsNotNull();
            Ensure.That(connectionSettingsRepository).IsNotNull();

            _connectionViewModelStrategy = connectionViewModelStrategy;
            _connections = new List<IConnectionViewModel>();
            _subscription = connectionSettingsRepository.Subscribe(this);
        }

        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <value>
        /// The connections.
        /// </value>
        public IEnumerable<IConnectionViewModel> Connections
        {
            get
            {
                return _connections;
            }

            private set
            {
                _connections = value.ToList();
                NotifyOfPropertyChange(() => Connections);
            }
        }

        /// <summary>
        /// Provides the observer with new data.
        /// </summary>
        /// <param name="value">The current notification information.</param>
        public void OnNext(Notification<ConnectionSettings>[] value)
        {
            if (_isDisposed)
            {
                // Do nothing if disposed (pattern practice).
                return;
            }

            var isDirty = false;

            var currentConnections = Connections.ToDictionary(connection => connection.SettingsId, connection => connection);

            foreach (var settings in Notification<ConnectionSettings>.GetPayloads(value, NotificationType.Removed, s => currentConnections.ContainsKey(s.Id)))
            {
                currentConnections.Remove(settings.Id);

                isDirty = true;
            }

            foreach (var settings in Notification<ConnectionSettings>.GetPayloads(value, NotificationType.Updated, s => currentConnections.ContainsKey(s.Id)))
            {
                var connection = currentConnections[settings.Id];

                connection.SettingsName = settings.Name;

                isDirty = true;
            }

            foreach (var settings in Notification<ConnectionSettings>.GetPayloads(value, NotificationType.Added, s => !currentConnections.ContainsKey(s.Id)))
            {
                var connection = _connectionViewModelStrategy.Create(settings);

                currentConnections.Add(settings.Id, connection);

                isDirty = true;
            }

            if (isDirty)
            {
                Connections = currentConnections.Values.OrderBy(connection => connection.SettingsName);
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

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (_subscription != null)
                {
                    _subscription.Dispose();
                    _subscription = null;
                }

                if (_connections != null)
                {
                    _connections.Clear();
                    _connections = null;
                }
            }

            _isDisposed = true;
        }
    }
}