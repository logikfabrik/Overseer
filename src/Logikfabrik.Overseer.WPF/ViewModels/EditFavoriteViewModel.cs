namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;
    using Favorites;

    public class EditFavoriteViewModel : PropertyChangedBase
    {
        private readonly IFavoritesRepository _favoritesRepository;
        private readonly Guid _settingsId;
        private readonly string _projectId;
        private bool _isFavorite;

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

            _favoritesRepository.Add(new Favorite { SettingsId = _settingsId, ProjectId = _projectId});

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
