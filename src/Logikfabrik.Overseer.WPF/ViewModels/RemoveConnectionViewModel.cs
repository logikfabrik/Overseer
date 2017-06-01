// <copyright file="RemoveConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Linq;
    using Caliburn.Micro;
    using EnsureThat;
    using Extensions;
    using Navigation;
    using Settings;

    /// <summary>
    /// The <see cref="RemoveConnectionViewModel" /> class.
    /// </summary>
    public class RemoveConnectionViewModel : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IConnectionSettingsRepository _settingsRepository;
        private readonly Guid _settingsId;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="settingsRepository">The settings repository.</param>
        /// <param name="settingsId">The settings identifier.</param>
        public RemoveConnectionViewModel(IEventAggregator eventAggregator, IConnectionSettingsRepository settingsRepository, Guid settingsId)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(settingsRepository).IsNotNull();
            Ensure.That(settingsId).IsNotEmpty();

            _eventAggregator = eventAggregator;
            _settingsRepository = settingsRepository;
            _settingsId = settingsId;
            DisplayName = "Remove connection";
        }

        /// <summary>
        /// Remove the connection.
        /// </summary>
        public void RemoveConnection()
        {
            _settingsRepository.Remove(_settingsId);

            var viewModel = this.GetViewModels<IConnectionViewModel>().Single(vm => vm.SettingsId == _settingsId);

            var from = new[] { new NavigationTarget(viewModel) };
            var to = new NavigationTarget(typeof(ConnectionsViewModel));

            var message = new NavigationMessage(to, from);

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
