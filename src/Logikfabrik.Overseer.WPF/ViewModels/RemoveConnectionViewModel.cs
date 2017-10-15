// <copyright file="RemoveConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using Navigation;
    using Settings;

    /// <summary>
    /// The <see cref="RemoveConnectionViewModel" /> class.
    /// </summary>
    public class RemoveConnectionViewModel : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IConnectionSettingsRepository _settingsRepository;
        private readonly IConnectionViewModel _connectionViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="settingsRepository">The settings repository.</param>
        /// <param name="connectionViewModel">The connection view model.</param>
        public RemoveConnectionViewModel(IEventAggregator eventAggregator, IConnectionSettingsRepository settingsRepository, IConnectionViewModel connectionViewModel)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(settingsRepository).IsNotNull();
            Ensure.That(connectionViewModel).IsNotNull();

            _eventAggregator = eventAggregator;
            _settingsRepository = settingsRepository;
            _connectionViewModel = connectionViewModel;
            DisplayName = Properties.Resources.RemoveConnection_View;
        }

        /// <summary>
        /// Remove the connection.
        /// </summary>
        public void Remove()
        {
            var settingsId = _connectionViewModel.SettingsId;

            _connectionViewModel.TryClose();

            _settingsRepository.Remove(settingsId);

            var message = new NavigationMessage(typeof(ConnectionsViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
