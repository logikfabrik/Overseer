// <copyright file="RemoveConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using Favorites;
    using JetBrains.Annotations;
    using Navigation;
    using Settings;

    /// <summary>
    /// The <see cref="RemoveConnectionViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class RemoveConnectionViewModel : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IConnectionSettingsRepository _connectionSettingsRepository;
        private readonly IFavoritesRepository _favoritesRepository;
        private readonly INavigationMessageFactory<ViewConnectionsViewModel> _navigationMessageFactory;
        private readonly IViewConnectionViewModel _viewConnectionViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveConnectionViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="connectionSettingsRepository">The connection settings repository.</param>
        /// <param name="favoritesRepository">The favorites repository.</param>
        /// <param name="navigationMessageFactory">The navigation message factory.</param>
        /// <param name="viewConnectionViewModel">The view connection view model.</param>
        [UsedImplicitly]

        // ReSharper disable once InheritdocConsiderUsage
        public RemoveConnectionViewModel(
            IPlatformProvider platformProvider,
            IEventAggregator eventAggregator,
            IConnectionSettingsRepository connectionSettingsRepository,
            IFavoritesRepository favoritesRepository,
            INavigationMessageFactory<ViewConnectionsViewModel> navigationMessageFactory,
            IViewConnectionViewModel viewConnectionViewModel)
            : base(platformProvider)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(connectionSettingsRepository).IsNotNull();
            Ensure.That(favoritesRepository).IsNotNull();
            Ensure.That(navigationMessageFactory).IsNotNull();
            Ensure.That(viewConnectionViewModel).IsNotNull();

            _eventAggregator = eventAggregator;
            _connectionSettingsRepository = connectionSettingsRepository;
            _favoritesRepository = favoritesRepository;
            _navigationMessageFactory = navigationMessageFactory;
            _viewConnectionViewModel = viewConnectionViewModel;
            DisplayName = Properties.Resources.RemoveConnection_View;
        }

        /// <summary>
        /// Remove the connection.
        /// </summary>
        public void Remove()
        {
            var settingsId = _viewConnectionViewModel.SettingsId;

            _viewConnectionViewModel.TryClose();

            _favoritesRepository.Remove(settingsId);
            _connectionSettingsRepository.Remove(settingsId);

            var message = _navigationMessageFactory.Create();

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
