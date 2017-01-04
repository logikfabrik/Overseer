// <copyright file="ConnectionPool.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="ConnectionPool" /> class.
    /// </summary>
    public class ConnectionPool : IConnectionPool
    {
        private readonly IDisposable _subscription;
        private readonly HashSet<IObserver<Connection[]>> _observers;
        private IDictionary<Guid, Connection> _connections;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionPool"/> class.
        /// </summary>
        /// <param name="settingsRepository">The settings repository.</param>
        public ConnectionPool(IConnectionSettingsRepository settingsRepository)
        {
            Ensure.That(settingsRepository).IsNotNull();

            _connections = new Dictionary<Guid, Connection>();
            _observers = new HashSet<IObserver<Connection[]>>();
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
        public void OnNext(ConnectionSettings[] value)
        {
            if (_isDisposed)
            {
                // Do nothing if disposed (pattern practice).
                return;
            }

            if (!value.Any())
            {
                _connections.Clear();
            }
            else
            {
                foreach (var settings in value)
                {
                    Connection connectionToUpdate;

                    if (_connections.TryGetValue(settings.Id, out connectionToUpdate))
                    {
                        if (connectionToUpdate.Settings.Equals(settings))
                        {
                            continue;
                        }

                        connectionToUpdate.Settings = settings;
                    }
                    else
                    {
                        var connectionToAdd = new Connection(settings);

                        _connections.Add(settings.Id, connectionToAdd);
                    }
                }

                var connectionsToRemove = _connections.Keys.Except(value.Select(s => s.Id));

                foreach (var id in connectionsToRemove)
                {
                    var connectionToRemove = _connections[id];

                    _connections.Remove(id);

                    connectionToRemove.Dispose();
                }
            }

            Next();
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
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Notifies the provider that an observer is to receive notifications.
        /// </summary>
        /// <param name="observer">The object that is to receive notifications.</param>
        /// <returns>
        /// A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them.
        /// </returns>
        public IDisposable Subscribe(IObserver<Connection[]> observer)
        {
            Ensure.That(observer).IsNotNull();

            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            // ReSharper disable once InvertIf
            if (_observers.Add(observer))
            {
                observer.OnNext(_connections.Values.ToArray());
            }

            return new Subscription<Connection[]>(_observers, observer);
        }

        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // ReSharper disable once InvertIf
            if (disposing)
            {
                _subscription?.Dispose();
                _observers.Clear();

                if (_connections != null)
                {
                    foreach (var connection in _connections.Values)
                    {
                        connection.Dispose();
                    }

                    _connections = null;
                }
            }

            _isDisposed = true;
        }

        private void Next()
        {
            var connections = _connections.Values.ToArray();

            foreach (var observer in _observers)
            {
                observer.OnNext(connections);
            }
        }
    }
}