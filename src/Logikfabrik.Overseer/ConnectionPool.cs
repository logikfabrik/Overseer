// <copyright file="ConnectionPool.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using Extensions;
    using Notification;
    using Settings;

    /// <summary>
    /// The <see cref="ConnectionPool" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ConnectionPool : IConnectionPool, IDisposable
    {
        private readonly IBuildProviderStrategy _buildProviderStrategy;
        private readonly NotificationFactory<IConnection> _notificationFactory;
        private HashSet<IObserver<Notification<IConnection>[]>> _observers;
        private IDictionary<Guid, Connection> _connections;
        private IDisposable _subscription;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionPool" /> class.
        /// </summary>
        /// <param name="connectionSettingsRepository">The connection settings repository.</param>
        /// <param name="buildProviderStrategy">The build provider strategy.</param>
        /// <param name="notificationFactory">The notification factory.</param>
        public ConnectionPool(IConnectionSettingsRepository connectionSettingsRepository, IBuildProviderStrategy buildProviderStrategy, NotificationFactory<IConnection> notificationFactory)
        {
            Ensure.That(connectionSettingsRepository).IsNotNull();
            Ensure.That(buildProviderStrategy).IsNotNull();
            Ensure.That(notificationFactory).IsNotNull();

            _buildProviderStrategy = buildProviderStrategy;
            _notificationFactory = notificationFactory;

            _connections = new Dictionary<Guid, Connection>();
            _observers = new HashSet<IObserver<Notification<IConnection>[]>>();
            _subscription = connectionSettingsRepository.Subscribe(this);
        }

        /// <summary>
        /// Gets the current connections.
        /// </summary>
        /// <value>
        /// The current connections.
        /// </value>
        internal IEnumerable<Connection> CurrentConnections => _connections.Values;

        /// <inheritdoc />
        public void OnNext(Notification<ConnectionSettings>[] value)
        {
            if (_isDisposed)
            {
                // Do nothing if disposed (pattern practice).
                return;
            }

            var notifications = new List<Notification<IConnection>>();
            var toDisposeOf = new List<Connection>();

            foreach (var settings in NotificationUtility.GetPayloads(value, NotificationType.Removed, s => _connections.ContainsKey(s.Id)))
            {
                Remove(notifications, settings, toDisposeOf);
            }

            foreach (var settings in NotificationUtility.GetPayloads(value, NotificationType.Updated, s => _connections.ContainsKey(s.Id)))
            {
                Update(notifications, settings, toDisposeOf);
            }

            foreach (var settings in NotificationUtility.GetPayloads(value, NotificationType.Added, s => !_connections.ContainsKey(s.Id)))
            {
                Add(notifications, settings);
            }

            Next(notifications.ToArray());

            foreach (var connection in toDisposeOf)
            {
                connection.Dispose();
            }
        }

        /// <inheritdoc />
        public void OnError(Exception error)
        {
            // Do nothing, even if disposed (pattern practice).
        }

        /// <inheritdoc />
        public void OnCompleted()
        {
            // Do nothing, even if disposed (pattern practice).
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        public IDisposable Subscribe(IObserver<Notification<IConnection>[]> observer)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(observer).IsNotNull();

            // ReSharper disable once InvertIf
            if (_observers.Add(observer))
            {
                var notifications = _notificationFactory.Create(NotificationType.Added, _connections.Values);

                if (notifications.Any())
                {
                    observer.OnNext(notifications);
                }
            }

            return new Subscription<Notification<IConnection>[]>(_observers, observer);
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

        private void Next(Notification<IConnection>[] notifications)
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

        private void Add(ICollection<Notification<IConnection>> notifications, ConnectionSettings settings)
        {
            var connection = new Connection(_buildProviderStrategy, settings);

            _connections.Add(settings.Id, connection);

            notifications.Add(_notificationFactory.Create(NotificationType.Added, connection));
        }

        private void Update(ICollection<Notification<IConnection>> notifications, ConnectionSettings settings, ICollection<Connection> toDisposeOf)
        {
            Remove(notifications, settings, toDisposeOf);
            Add(notifications, settings);
        }

        private void Remove(ICollection<Notification<IConnection>> notifications, ConnectionSettings settings, ICollection<Connection> toDisposeOf)
        {
            var connection = _connections[settings.Id];

            toDisposeOf.Add(connection);

            _connections.Remove(settings.Id);

            notifications.Add(_notificationFactory.Create(NotificationType.Removed, connection));
        }
    }
}