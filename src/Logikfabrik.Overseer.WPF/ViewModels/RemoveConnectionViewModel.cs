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
    // ReSharper disable once InheritdocConsiderUsage
    public class RemoveConnectionViewModel : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IConnectionSettingsRepository _connectionSettingsRepository;
        private readonly IViewConnectionViewModel _viewConnectionViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveConnectionViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="connectionSettingsRepository">The connection settings repository.</param>
        /// <param name="viewConnectionViewModel">The view connection view model.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public RemoveConnectionViewModel(IPlatformProvider platformProvider, IEventAggregator eventAggregator, IConnectionSettingsRepository connectionSettingsRepository, IViewConnectionViewModel viewConnectionViewModel)
            : base(platformProvider)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(connectionSettingsRepository).IsNotNull();
            Ensure.That(viewConnectionViewModel).IsNotNull();

            _eventAggregator = eventAggregator;
            _connectionSettingsRepository = connectionSettingsRepository;
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

            _connectionSettingsRepository.Remove(settingsId);

            var message = new NavigationMessage(typeof(ViewConnectionsViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
