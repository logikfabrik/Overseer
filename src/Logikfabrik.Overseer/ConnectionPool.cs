// <copyright file="ConnectionPool.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using Extensions;
    using Settings;

    /// <summary>
    /// The <see cref="ConnectionPool" /> class.
    /// </summary>
    public class ConnectionPool : IConnectionPool, IDisposable
    {
        private readonly IBuildProviderStrategy _buildProviderStrategy;
        private HashSet<IObserver<Notification<Connection>[]>> _observers;
        private IDictionary<Guid, Connection> _connections;
        private IDisposable _subscription;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionPool" /> class.
        /// </summary>
        /// <param name="settingsRepository">The settings repository.</param>
        /// <param name="buildProviderStrategy">The build provider strategy.</param>
        public ConnectionPool(IConnectionSettingsRepository settingsRepository, IBuildProviderStrategy buildProviderStrategy)
        {
            Ensure.That(settingsRepository).IsNotNull();
            Ensure.That(buildProviderStrategy).IsNotNull();

            _buildProviderStrategy = buildProviderStrategy;
            _connections = new Dictionary<Guid, Connection>();
            _observers = new HashSet<IObserver<Notification<Connection>[]>>();
            _subscription = settingsRepository.Subscribe(this);
        }

        /// <summary>
        /// Gets the current connections.
        /// </summary>
        /// <value>
        /// The current connections.
        /// </value>
        internal IEnumerable<Connection> CurrentConnections => _connections.Values;

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

            var notifications = new List<Notification<Connection>>();
            var toDisposeOf = new List<Connection>();

            foreach (var settings in Notification<ConnectionSettings>.GetPayloads(value, NotificationType.Removed, s => _connections.ContainsKey(s.Id)))
            {
                Remove(notifications, settings, toDisposeOf);
            }

            foreach (var settings in Notification<ConnectionSettings>.GetPayloads(value, NotificationType.Updated, s => _connections.ContainsKey(s.Id)))
            {
                Update(notifications, settings, toDisposeOf);
            }

            foreach (var settings in Notification<ConnectionSettings>.GetPayloads(value, NotificationType.Added, s => !_connections.ContainsKey(s.Id)))
            {
                Add(notifications, settings);
            }

            Next(notifications.ToArray());

            foreach (var connection in toDisposeOf)
            {
                connection.Dispose();
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
        /// Notifies the provider that an observer is to receive notifications.
        /// </summary>
        /// <param name="observer">The object that is to receive notifications.</param>
        /// <returns>
        /// A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them.
        /// </returns>
        public IDisposable Subscribe(IObserver<Notification<Connection>[]> observer)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(observer).IsNotNull();

            // ReSharper disable once InvertIf
            if (_observers.Add(observer))
            {
                var notifications = Notification<Connection>.Create(NotificationType.Added, _connections.Values);

                if (notifications.Any())
                {
                    observer.OnNext(notifications);
                }
            }

            return new Subscription<Notification<Connection>[]>(_observers, observer);
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

                if (_observers != null)
                {
                    _observers.Clear();
                    _observers = null;
                }

                if (_connections != null)
                {
                    foreach (var connection in _connections.Values)
                    {
                        connection.Dispose();
                    }

                    _connections.Clear();
                    _connections = null;
                }
            }

            _isDisposed = true;
        }

        private void Next(Notification<Connection>[] notifications)
        {
            if (!notifications.Any())
            {
                return;
            }

            foreach (var observer in _observers)
            {
                observer.OnNext(notifications);
            }
        }

        private void Add(ICollection<Notification<Connection>> notifications, ConnectionSettings settings)
        {
            var connection = new Connection(_buildProviderStrategy, settings);

            _connections.Add(settings.Id, connection);

            notifications.Add(Notification<Connection>.Create(NotificationType.Added, connection));
        }

        private void Update(ICollection<Notification<Connection>> notifications, ConnectionSettings settings, ICollection<Connection> toDisposeOf)
        {
            Remove(notifications, settings, toDisposeOf);
            Add(notifications, settings);
        }

        private void Remove(ICollection<Notification<Connection>> notifications, ConnectionSettings settings, ICollection<Connection> toDisposeOf)
        {
            var connection = _connections[settings.Id];

            toDisposeOf.Add(connection);

            _connections.Remove(settings.Id);

            notifications.Add(Notification<Connection>.Create(NotificationType.Removed, connection));
        }
    }
}