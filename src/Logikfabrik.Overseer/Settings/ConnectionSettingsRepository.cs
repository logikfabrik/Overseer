﻿// <copyright file="ConnectionSettingsRepository.cs" company="Logikfabrik">
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
    // ReSharper disable once InheritdocConsiderUsage
    public class ConnectionSettingsRepository : IConnectionSettingsRepository, IDisposable
    {
        private readonly IConnectionSettingsStore _settingsStore;
        private HashSet<IObserver<Notification<ConnectionSettings>[]>> _observers;
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
            _observers = new HashSet<IObserver<Notification<ConnectionSettings>[]>>();
            _settings = _settingsStore.Load().ToDictionary(settings => settings.Id, settings => settings);
        }

        /// <inheritdoc />
        public void Add(ConnectionSettings settings)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(settings).IsNotNull();
            Ensure.That(() => _settings.ContainsKey(settings.Id), nameof(settings)).IsFalse();

            var clone = settings.Clone();

            _settings.Add(clone.Id, clone);

            Save();

            Next(NotificationType.Added, clone.Clone());
        }

        /// <inheritdoc />
        public void Update(ConnectionSettings settings)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(settings).IsNotNull();
            Ensure.That(() => _settings.ContainsKey(settings.Id), nameof(settings)).IsTrue();

            var clone = settings.Clone();

            _settings[clone.Id] = clone;

            Save();

            Next(NotificationType.Updated, clone.Clone());
        }

        /// <inheritdoc />
        public void Remove(Guid id)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(id).IsNotEmpty();
            Ensure.That(() => _settings.ContainsKey(id), nameof(id)).IsTrue();

            var clone = _settings[id].Clone();

            _settings.Remove(id);

            Save();

            Next(NotificationType.Removed, clone);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        public ConnectionSettings Get(Guid id)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(id).IsNotEmpty();

            ConnectionSettings settings;

            return _settings.TryGetValue(id, out settings) ? settings.Clone() : null;
        }

        /// <inheritdoc />
        public IDisposable Subscribe(IObserver<Notification<ConnectionSettings>[]> observer)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(observer).IsNotNull();

            // ReSharper disable once InvertIf
            if (_observers.Add(observer))
            {
                var notifications = Notification<ConnectionSettings>.Create(NotificationType.Added, _settings.Values.Select(s => s.Clone()));

                if (notifications.Any())
                {
                    observer.OnNext(notifications);
                }
            }

            return new Subscription<Notification<ConnectionSettings>[]>(_observers, observer);
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

        private void Next(NotificationType type, ConnectionSettings settings)
        {
            var notifications = new[] { Notification<ConnectionSettings>.Create(type, settings) };

            foreach (var observer in _observers)
            {
                observer.OnNext(notifications);
            }
        }

        private void Save()
        {
            _settingsStore.Save(_settings.Values.ToArray());
        }
    }
}