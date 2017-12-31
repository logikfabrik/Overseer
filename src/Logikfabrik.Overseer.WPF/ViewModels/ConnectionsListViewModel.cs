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
    using Navigation;
    using Settings;

    /// <summary>
    /// The <see cref="ConnectionsListViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ConnectionsListViewModel : PropertyChangedBase, IObserver<Notification<ConnectionSettings>[]>, IDisposable
    {
        private readonly IApp _application;
        private readonly IEventAggregator _eventAggregator;
        private readonly IConnectionViewModelStrategy _connectionViewModelStrategy;
        private BindableCollection<IConnectionViewModel> _connections;
        private IDisposable _subscription;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionsListViewModel" /> class.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="connectionViewModelStrategy">The connection view model strategy.</param>
        /// <param name="connectionSettingsRepository">The connection settings repository.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public ConnectionsListViewModel(
            IApp application,
            IEventAggregator eventAggregator,
            IConnectionViewModelStrategy connectionViewModelStrategy,
            IConnectionSettingsRepository connectionSettingsRepository)
        {
            Ensure.That(application).IsNotNull();
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(connectionViewModelStrategy).IsNotNull();
            Ensure.That(connectionSettingsRepository).IsNotNull();

            _application = application;
            _eventAggregator = eventAggregator;
            _connectionViewModelStrategy = connectionViewModelStrategy;
            _connections = new BindableCollection<IConnectionViewModel>();
            _subscription = connectionSettingsRepository.Subscribe(this);
        }

        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <value>
        /// The connections.
        /// </value>
        public IEnumerable<IConnectionViewModel> Connections => _connections;

        /// <summary>
        /// Gets a value indicating whether this instance has connections.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has connections; otherwise, <c>false</c>.
        /// </value>
        public bool HasConnections => _connections.Any();

        /// <summary>
        /// Gets a value indicating whether this instance has no connections.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has no connections; otherwise, <c>false</c>.
        /// </value>
        public bool HasNoConnections => !_connections.Any();

        /// <inheritdoc />
        public void OnNext(Notification<ConnectionSettings>[] value)
        {
            if (_isDisposed)
            {
                // Do nothing if disposed (pattern practice).
                return;
            }

            var currentConnections = Connections.ToDictionary(connection => connection.SettingsId, connection => connection);

            foreach (var settings in Notification<ConnectionSettings>.GetPayloads(value, NotificationType.Removed, s => currentConnections.ContainsKey(s.Id)))
            {
                var connectionToRemove = currentConnections[settings.Id];

                _application.Dispatcher.Invoke(() =>
                {
                    _connections.Remove(connectionToRemove);
                });
            }

            foreach (var settings in Notification<ConnectionSettings>.GetPayloads(value, NotificationType.Updated, s => currentConnections.ContainsKey(s.Id)))
            {
                var connectionToUpdate = currentConnections[settings.Id];

                connectionToUpdate.SettingsName = settings.Name;
            }

            foreach (var settings in Notification<ConnectionSettings>.GetPayloads(value, NotificationType.Added, s => !currentConnections.ContainsKey(s.Id)))
            {
                _application.Dispatcher.Invoke(() =>
                {
                    var connectionToAdd = _connectionViewModelStrategy.Create(settings);

                    var names = _connections.Select(c => c.SettingsName).Concat(new[] { connectionToAdd.SettingsName }).OrderBy(name => name).ToArray();

                    var index = Array.IndexOf(names, connectionToAdd.SettingsName);

                    _connections.Insert(index, connectionToAdd);
                });
            }
        }

        public void Add()
        {
            var message = new NavigationMessage(typeof(BuildProvidersViewModel));

            _eventAggregator.PublishOnUIThread(message);
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