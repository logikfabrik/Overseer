﻿// <copyright file="AddConnectionViewModel{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="AddConnectionViewModel{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The <see cref="ConnectionSettings" /> type.</typeparam>
    public abstract class AddConnectionViewModel<T> : ValidationViewModel
        where T : ConnectionSettings
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IConnectionSettingsRepository _settingsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionViewModel{T}" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="settingsRepository">The settings repository.</param>
        protected AddConnectionViewModel(IEventAggregator eventAggregator, IConnectionSettingsRepository settingsRepository)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(settingsRepository).IsNotNull();

            _eventAggregator = eventAggregator;
            _settingsRepository = settingsRepository;
        }

        /// <summary>
        /// Gets or sets the connection name.
        /// </summary>
        /// <value>
        /// The connection name.
        /// </value>
        public string ConnectionName { get; set; }

        /// <summary>
        /// Add the connection.
        /// </summary>
        public void AddConnection()
        {
            if (!Validator.Validate(this).IsValid)
            {
                return;
            }

            _settingsRepository.Add(GetSettings());

            ViewConnections();
        }

        /// <summary>
        /// View the connections.
        /// </summary>
        public void ViewConnections()
        {
            var message = new NavigationMessage(typeof(ConnectionsViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns>
        /// The settings.
        /// </returns>
        protected abstract T GetSettings();
    }
}