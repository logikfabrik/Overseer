﻿// <copyright file="EditConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="EditConnectionViewModel" /> class.
    /// </summary>
    public abstract class EditConnectionViewModel : ValidationViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IBuildProviderSettingsRepository _buildProviderSettingsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildProviderSettingsRepository">The build provider settings repository.</param>
        protected EditConnectionViewModel(IEventAggregator eventAggregator, IBuildProviderSettingsRepository buildProviderSettingsRepository)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildProviderSettingsRepository).IsNotNull();

            _eventAggregator = eventAggregator;
            _buildProviderSettingsRepository = buildProviderSettingsRepository;
        }

        /// <summary>
        /// Gets or sets the connection name.
        /// </summary>
        /// <value>
        /// The connection name.
        /// </value>
        public string ConnectionName { get; set; }

        /// <summary>
        /// Edit the connection.
        /// </summary>
        public void EditConnection()
        {
            if (!Validator.Validate(this).IsValid)
            {
                return;
            }

            _buildProviderSettingsRepository.Add(GetSettings());

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
        /// <returns>The settings.</returns>
        protected abstract BuildProviderSettings GetSettings();
    }
}