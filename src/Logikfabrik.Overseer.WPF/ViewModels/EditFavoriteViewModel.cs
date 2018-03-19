// <copyright file="EditFavoriteViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;
    using Favorites;

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

        public bool IsNotFavorite => !IsFavorite;

        public void Add()
        {
            if (_isFavorite)
            {
                return;
            }

            _favoritesRepository.Add(new Favorite { SettingsId = _settingsId, ProjectId = _projectId });

            IsFavorite = true;
        }

        public void Remove()
        {
            if (!_isFavorite)
            {
                return;
            }

            _favoritesRepository.Remove(_settingsId, _projectId);

            IsFavorite = false;
        }
    }
}