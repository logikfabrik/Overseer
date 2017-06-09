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
    using Settings.Extensions;

    /// <summary>
    /// The <see cref="ConnectionPool" /> class.
    /// </summary>
    public class ConnectionPool : IConnectionPool, IDisposable
    {
        private readonly IBuildProviderStrategy _buildProviderStrategy;
        private HashSet<IObserver<Connection[]>> _observers;
        private IDictionary<Guid, Connection> _connections;
        private IDisposable _subscription;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionPool"/> class.
        /// </summary>
        /// <param name="settingsRepository">The settings repository.</param>
        /// <param name="buildProviderStrategy">The build provider strategy.</param>
        public ConnectionPool(IConnectionSettingsRepository settingsRepository, IBuildProviderStrategy buildProviderStrategy)
        {
            Ensure.That(settingsRepository).IsNotNull();
            Ensure.That(buildProviderStrategy).IsNotNull();

            _buildProviderStrategy = buildProviderStrategy;
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

            var connectionsToDispose = new List<Connection>();

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
                        if (connectionToUpdate.Settings.Signature() == settings.Signature())
                        {
                            continue;
                        }

                        connectionToUpdate.Settings = settings;
                    }
                    else
                    {
                        var connectionToAdd = new Connection(_buildProviderStrategy, settings);

                        _connections.Add(settings.Id, connectionToAdd);
                    }
                }

                var connectionsToRemove = _connections.Keys.Except(value.Select(s => s.Id)).ToArray();

                foreach (var id in connectionsToRemove)
                {
                    var connection = _connections[id];

                    connectionsToDispose.Add(connection);

                    _connections.Remove(id);
                }
            }

            Next();

            foreach (var connection in connectionsToDispose)
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
        public IDisposable Subscribe(IObserver<Connection[]> observer)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(observer).IsNotNull();

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