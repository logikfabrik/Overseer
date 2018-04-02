// <copyright file="FavoritesViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using Caliburn.Micro;
    using EnsureThat;
    using Factories;
    using Favorites;
    using Notification;

    /// <summary>
    /// The <see cref="FavoritesViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class FavoritesViewModel : PropertyChangedBase, IObserver<Notification<Favorite>[]>, IDisposable
    {
        private readonly IViewProjectViewModelFactory _viewProjectViewModelFactory;
        private BindableCollection<IViewProjectViewModel> _favorites;
        private IDisposable _subscription;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritesViewModel" /> class.
        /// </summary>
        /// <param name="favoritesRepository">The favorites repository.</param>
        /// <param name="viewProjectViewModelFactory">The view project view model factory.</param>
        /// <param name="buildTracker">The build tracker.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public FavoritesViewModel(
            IFavoritesRepository favoritesRepository,
            IViewProjectViewModelFactory viewProjectViewModelFactory,
            IBuildTracker buildTracker)
        {
            Ensure.That(favoritesRepository).IsNotNull();
            Ensure.That(viewProjectViewModelFactory).IsNotNull();

            _viewProjectViewModelFactory = viewProjectViewModelFactory;
            _favorites = new BindableCollection<IViewProjectViewModel>();
            _subscription = favoritesRepository.Subscribe(this);

            WeakEventManager<IBuildTracker, BuildTrackerConnectionErrorEventArgs>.AddHandler(buildTracker, nameof(buildTracker.ConnectionError), BuildTrackerConnectionError);
            WeakEventManager<IBuildTracker, BuildTrackerProjectErrorEventArgs>.AddHandler(buildTracker, nameof(buildTracker.ProjectError), BuildTrackerProjectError);
            WeakEventManager<IBuildTracker, BuildTrackerProjectProgressEventArgs>.AddHandler(buildTracker, nameof(buildTracker.ProjectProgressChanged), BuildTrackerProjectProgressChanged);
        }

        /// <summary>
        /// Gets the favorites.
        /// </summary>
        /// <value>
        /// The favorites.
        /// </value>
        public IEnumerable<IViewProjectViewModel> Favorites => _favorites;

        /// <summary>
        /// Gets a value indicating whether this instance has favorites.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has favorites; otherwise, <c>false</c>.
        /// </value>
        public bool HasFavorites => _favorites.Any();

        /// <summary>
        /// Gets a value indicating whether this instance has no favorites.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has no favorites; otherwise, <c>false</c>.
        /// </value>
        public bool HasNoFavorites => !_favorites.Any();

        /// <inheritdoc />
        public void OnNext(Notification<Favorite>[] value)
        {
            if (_isDisposed)
            {
                // Do nothing if disposed (pattern practice).
                return;
            }

            var currentFavorites = Favorites.ToDictionary(favorite => new Tuple<Guid, string>(favorite.SettingsId, favorite.Id), favorite => favorite);

            foreach (var favorite in NotificationUtility.GetPayloads(value, NotificationType.Removed, f => currentFavorites.ContainsKey(new Tuple<Guid, string>(f.SettingsId, f.ProjectId))))
            {
                var favoriteToRemove = currentFavorites[new Tuple<Guid, string>(favorite.SettingsId, favorite.ProjectId)];

                OnUIThread(() =>
                {
                    _favorites.Remove(favoriteToRemove);
                });
            }

            foreach (var favorite in NotificationUtility.GetPayloads(value, NotificationType.Added, f => !currentFavorites.ContainsKey(new Tuple<Guid, string>(f.SettingsId, f.ProjectId))))
            {
                OnUIThread(() =>
                {
                    var favoriteToAdd = _viewProjectViewModelFactory.Create(favorite.SettingsId, favorite.ProjectId);

                    _favorites.Add(favoriteToAdd);
                });
            }

            NotifyOfPropertyChange(() => HasFavorites);
            NotifyOfPropertyChange(() => HasNoFavorites);
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

                if (_favorites != null)
                {
                    _favorites.Clear();
                    _favorites = null;
                }
            }

            _isDisposed = true;
        }

        private void BuildTrackerConnectionError(object sender, BuildTrackerConnectionErrorEventArgs e)
        {
            var favorites = _favorites.Where(f => f.SettingsId == e.SettingsId).ToArray();

            if (!favorites.Any())
            {
                return;
            }

            foreach (var favorite in favorites)
            {
                favorite.IsBusy = false;
                favorite.IsErrored = true;
            }
        }

        private void BuildTrackerProjectError(object sender, BuildTrackerProjectErrorEventArgs e)
        {
            var favorite = _favorites.SingleOrDefault(f => f.SettingsId == e.SettingsId && f.Id == e.Project.Id);

            if (favorite == null)
            {
                return;
            }

                favorite.IsBusy = false;
                favorite.IsErrored = true;
        }

        private void BuildTrackerProjectProgressChanged(object sender, BuildTrackerProjectProgressEventArgs e)
        {
            var favorite = _favorites.SingleOrDefault(f => f.SettingsId == e.SettingsId && f.Id == e.Project.Id);

            if (favorite == null)
            {
                return;
            }

            favorite.IsBusy = false;
            favorite.Name = e.Project.Name;

            // TODO: Set build stats for favorite.
        }
    }
}
