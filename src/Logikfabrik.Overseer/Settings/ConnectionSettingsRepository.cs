// <copyright file="ConnectionSettingsRepository.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using Extensions;

    /// <summary>
    /// The <see cref="ConnectionSettingsRepository" /> class.
    /// </summary>
    public class ConnectionSettingsRepository : IConnectionSettingsRepository, IDisposable
    {
        private readonly IConnectionSettingsStore _settingsStore;
        private HashSet<IObserver<ConnectionSettings[]>> _observers;
        private IDictionary<Guid, ConnectionSettings> _settings;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsRepository" /> class.
        /// </summary>
        /// <param name="settingsStore">The settings store.</param>
        public ConnectionSettingsRepository(IConnectionSettingsStore settingsStore)
        {
            Ensure.That(settingsStore).IsNotNull();

            _settingsStore = settingsStore;
            _observers = new HashSet<IObserver<ConnectionSettings[]>>();
            _settings = _settingsStore.Load().ToDictionary(settings => settings.Id, settings => settings);
        }

        /// <summary>
        /// Adds the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Add(ConnectionSettings settings)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(settings).IsNotNull();

            var clone = settings.Clone();

            _settings.Add(clone.Id, clone);

            Save();

            Next();
        }

        /// <summary>
        /// Removes the settings with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Remove(Guid id)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(id).IsNotEmpty();

            if (!_settings.Remove(id))
            {
                return;
            }

            Save();

            Next();
        }

        /// <summary>
        /// Updates the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Update(ConnectionSettings settings)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(settings).IsNotNull();

            if (settings.Equals(_settings[settings.Id]))
            {
                return;
            }

            var clone = settings.Clone();

            _settings[clone.Id] = clone;

            Save();

            Next();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Gets the settings with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The settings with the specified identifier.
        /// </returns>
        public ConnectionSettings Get(Guid id)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(id).IsNotEmpty();

            ConnectionSettings settings;

            return _settings.TryGetValue(id, out settings) ? settings.Clone() : null;
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns>
        /// The settings.
        /// </returns>
        public IEnumerable<ConnectionSettings> Get()
        {
            this.ThrowIfDisposed(_isDisposed);

            var clones = _settings.Values.Select(settings => settings.Clone()).ToArray();

            return clones;
        }

        /// <summary>
        /// Notifies the provider that an observer is to receive notifications.
        /// </summary>
        /// <param name="observer">The object that is to receive notifications.</param>
        /// <returns>
        /// A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them.
        /// </returns>
        public IDisposable Subscribe(IObserver<ConnectionSettings[]> observer)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(observer).IsNotNull();

            // ReSharper disable once InvertIf
            if (_observers.Add(observer))
            {
                var clones = _settings.Values.Select(settings => settings.Clone()).ToArray();

                observer.OnNext(clones);
            }

            return new Subscription<ConnectionSettings[]>(_observers, observer);
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
                if (_observers != null)
                {
                    _observers.Clear();
                    _observers = null;

                    _settings.Clear();
                    _settings = null;
                }
            }

            _isDisposed = true;
        }

        private void Next()
        {
            var clones = _settings.Values.Select(settings => settings.Clone()).ToArray();

            foreach (var observer in _observers)
            {
                observer.OnNext(clones);
            }
        }

        private void Save()
        {
            _settingsStore.Save(_settings.Values.ToArray());
        }
    }
}