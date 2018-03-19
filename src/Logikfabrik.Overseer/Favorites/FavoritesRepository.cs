// <copyright file="FavoritesRepository.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Favorites
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using Extensions;

    /// <summary>
    /// The <see cref="FavoritesRepository" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class FavoritesRepository : IFavoritesRepository, IDisposable
    {
        private readonly IFavoritesStore _favoritesStore;
        private HashSet<IObserver<Notification<Favorite>[]>> _observers;
        private IDictionary<Tuple<Guid, string>, Favorite> _favorites;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritesRepository" /> class.
        /// </summary>
        /// <param name="favoritesStore">The favorites store.</param>
        public FavoritesRepository(IFavoritesStore favoritesStore)
        {
            Ensure.That(favoritesStore).IsNotNull();

            _favoritesStore = favoritesStore;
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

            var id = new Tuple<Guid, string>(settingsId, projectId);

            var clone = _favorites[id].Clone();

            _favorites.Remove(id);

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
        public IDisposable Subscribe(IObserver<Notification<Favorite>[]> observer)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(observer).IsNotNull();

            // ReSharper disable once InvertIf
            if (_observers.Add(observer))
            {
                var notifications = Notification<Favorite>.Create(NotificationType.Added, _favorites.Values.Select(f => f.Clone()));

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
            var notifications = new[] { Notification<Favorite>.Create(type, favorite) };

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
