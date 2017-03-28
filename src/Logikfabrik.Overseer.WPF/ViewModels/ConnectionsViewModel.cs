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
    using Factories;
    using Settings;

    /// <summary>
    /// The <see cref="ConnectionsViewModel" /> class.
    /// </summary>
    public class ConnectionsViewModel : ViewModel, IObserver<ConnectionSettings[]>, IDisposable
    {
        private readonly IDisposable _subscription;
        private readonly IEventAggregator _eventAggregator;
        private readonly IConnectionViewModelStrategy _connectionViewModelStrategy;
        private readonly List<IConnectionViewModel> _connections;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionsViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="connectionSettingsRepository">The connection settings repository.</param>
        /// <param name="connectionViewModelStrategy">The connection view model strategy.</param>
        public ConnectionsViewModel(
            IEventAggregator eventAggregator,
            IConnectionSettingsRepository connectionSettingsRepository,
            IConnectionViewModelStrategy connectionViewModelStrategy)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(connectionSettingsRepository).IsNotNull();
            Ensure.That(connectionViewModelStrategy).IsNotNull();

            _eventAggregator = eventAggregator;
            _connectionViewModelStrategy = connectionViewModelStrategy;
            _connections = new List<IConnectionViewModel>();
            _subscription = connectionSettingsRepository.Subscribe(this);
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
        public IEnumerable<IConnectionViewModel> Connections => _connections;

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
            if (_isDisposed)
            {
                // Do nothing if disposed (pattern practice).
                return;
            }

            var isDirty = false;

            foreach (var settings in value)
            {
                var connectionToUpdate = _connections.SingleOrDefault(connection => connection.SettingsId == settings.Id);

                if (connectionToUpdate != null)
                {
                    connectionToUpdate.SettingsName = settings.Name;
                }
                else
                {
                    var connectionToAdd = _connectionViewModelStrategy.Create(settings);

                    _connections.Add(connectionToAdd);

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

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
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

            // ReSharper disable once UseNullPropagation
            if (disposing)
            {
                _subscription.Dispose();
            }

            _isDisposed = true;
        }
    }
}
