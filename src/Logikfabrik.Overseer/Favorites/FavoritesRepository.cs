﻿// <copyright file="FavoritesRepository.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Favorites
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using Extensions;
    using JetBrains.Annotations;
    using Notification;

    /// <summary>
    /// The <see cref="FavoritesRepository" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class FavoritesRepository : IFavoritesRepository, IDisposable
    {
        private readonly IFavoritesStore _favoritesStore;
        private readonly NotificationFactory<Favorite> _notificationFactory;
        private HashSet<IObserver<Notification<Favorite>[]>> _observers;
        private IDictionary<Tuple<Guid, string>, Favorite> _favorites;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritesRepository" /> class.
        /// </summary>
        /// <param name="favoritesStore">The favorites store.</param>
        /// <param name="notificationFactory">The notification factory.</param>
        [UsedImplicitly]
        public FavoritesRepository(IFavoritesStore favoritesStore, NotificationFactory<Favorite> notificationFactory)
        {
            Ensure.That(favoritesStore).IsNotNull();
            Ensure.That(notificationFactory).IsNotNull();

            _favoritesStore = favoritesStore;
            _notificationFactory = notificationFactory;

            _observers = new HashSet<IObserver<Notification<Favorite>[]>>();
            _favorites = _favoritesStore.Load().ToDictionary(favorite => new Tuple<Guid, string>(favorite.SettingsId, favorite.ProjectId), favorite => favorite);
        }

        /// <inheritdoc />
        public void Add(Favorite favorite)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(favorite).IsNotNull();
            Ensure.That(() => _favorites.ContainsKey(new Tuple<Guid, string>(favorite.SettingsId, favorite.ProjectId)), nameof(favorite)).IsFalse();

            var clone = favorite.Clone();

            _favorites.Add(new Tuple<Guid, string>(clone.SettingsId, clone.ProjectId), clone);

            Save();

            Next(NotificationType.Added, clone.Clone());
        }

        /// <inheritdoc />
        public void Remove(Guid settingsId, string projectId)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(settingsId).IsNotEmpty();
            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            var key = new Tuple<Guid, string>(settingsId, projectId);

            var clone = _favorites[key].Clone();

            _favorites.Remove(key);

            Save();

            Next(NotificationType.Removed, clone);
        }

        /// <inheritdoc />
        public void Remove(Guid settingsId)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(settingsId).IsNotEmpty();

            var keys = _favorites.Keys.Where(key => key.Item1 == settingsId).ToArray();

            var clones = _favorites.Where(pair => keys.Contains(pair.Key)).Select(pair => pair.Value.Clone()).ToArray();

            foreach (var key in keys)
            {
                _favorites.Remove(key);
            }

            Save();

            Next(NotificationType.Removed, clones);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        public IDisposable Subscribe(IObserver<Notification<Favorite>[]> observer)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(observer).IsNotNull();

            // ReSharper disable once InvertIf
            if (_observers.Add(observer))
            {
                var notifications = _notificationFactory.Create(NotificationType.Added, _favorites.Values.Select(f => f.Clone()));

                if (notifications.Any())
                {
                    observer.OnNext(notifications);
                }
            }

            return new Subscription<Notification<Favorite>[]>(_observers, observer);
        }

        /// <inheritdoc />
        public bool Exists(Guid settingsId, string projectId)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(settingsId).IsNotEmpty();
            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            var id = new Tuple<Guid, string>(settingsId, projectId);

            return _favorites.ContainsKey(id);
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
                }

                if (_favorites != null)
                {
                    _favorites.Clear();
                    _favorites = null;
                }
            }

            _isDisposed = true;
        }

        private void Next(NotificationType type, Favorite favorite)
        {
            var notifications = new[] { _notificationFactory.Create(type, favorite) };

            foreach (var observer in _observers)
            {
                observer.OnNext(notifications);
            }
        }

        private void Next(NotificationType type, IEnumerable<Favorite> favorites)
        {
            var notifications = _notificationFactory.Create(type, favorites);

            foreach (var observer in _observers)
            {
                observer.OnNext(notifications);
            }
        }

        private void Save()
        {
            _favoritesStore.Save(_favorites.Values.ToArray());
        }
    }
}
