// <copyright file="EditConnectionViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.ViewModels.Factories
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;
    using Settings;
    using WPF.ViewModels;
    using WPF.ViewModels.Factories;

    /// <summary>
    /// The <see cref="EditConnectionViewModelFactory" /> class.
    /// </summary>
    public sealed class EditConnectionViewModelFactory : IEditConnectionViewModelFactory<AppVeyor.ConnectionSettings>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IConnectionSettingsRepository _settingsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionViewModelFactory" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="settingsRepository">The settings repository.</param>
        public EditConnectionViewModelFactory(IEventAggregator eventAggregator, IConnectionSettingsRepository settingsRepository)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(settingsRepository).IsNotNull();

            _eventAggregator = eventAggregator;
            _settingsRepository = settingsRepository;
        }

        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        EditConnectionViewModel<AppVeyor.ConnectionSettings> IEditConnectionViewModelFactory<AppVeyor.ConnectionSettings>.Create(Guid settingsId)
        {
            Ensure.That(settingsId).IsNotEmpty();

            var settings = _settingsRepository.Get(settingsId) as AppVeyor.ConnectionSettings;

            return new EditConnectionViewModel(_eventAggregator, _settingsRepository, settings);
        }
    }
}
