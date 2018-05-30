// <copyright file="EditFavoriteViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;
    using Favorites;
    using JetBrains.Annotations;

    /// <summary>
    /// The <see cref="EditFavoriteViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class EditFavoriteViewModel : PropertyChangedBase
    {
        private readonly IFavoritesRepository _favoritesRepository;
        private readonly Guid _settingsId;
        private readonly string _projectId;
        private bool _isFavorite;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditFavoriteViewModel" /> class.
        /// </summary>
        /// <param name="favoritesRepository">The favorites repository.</param>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        [UsedImplicitly]

        // ReSharper disable once InheritdocConsiderUsage
        public EditFavoriteViewModel(IFavoritesRepository favoritesRepository, Guid settingsId, string projectId)
        {
            Ensure.That(favoritesRepository).IsNotNull();
            Ensure.That(settingsId).IsNotEmpty();
            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            _favoritesRepository = favoritesRepository;
            _settingsId = settingsId;
            _projectId = projectId;
            _isFavorite = _favoritesRepository.Exists(_settingsId, _projectId);
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a favorite.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is a favorite; otherwise, <c>false</c>.
        /// </value>
        public bool IsFavorite
        {
            get
            {
                return _isFavorite;
            }

            private set
            {
                _isFavorite = value;
                NotifyOfPropertyChange(() => IsFavorite);
                NotifyOfPropertyChange(() => IsNotFavorite);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is not a favorite.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is not a favorite; otherwise, <c>false</c>.
        /// </value>
        public bool IsNotFavorite => !IsFavorite;

        [UsedImplicitly]
        public void ToggleFavorite()
        {
            if (IsFavorite)
            {
                _favoritesRepository.Remove(_settingsId, _projectId);

                IsFavorite = false;
            }
            else
            {
                _favoritesRepository.Add(new Favorite { SettingsId = _settingsId, ProjectId = _projectId });

                IsFavorite = true;
            }
        }
    }
}