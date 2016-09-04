// <copyright file="AddConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Caliburn.Micro;
    using Settings;

    /// <summary>
    /// The <see cref="AddConnectionViewModel" /> class.
    /// </summary>
    public abstract class AddConnectionViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IBuildProviderSettingsRepository _buildProviderSettingsRepository;
        private string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildProviderSettingsRepository">The build provider settings repository.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="eventAggregator" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="buildProviderSettingsRepository" /> is <c>null</c>.</exception>
        protected AddConnectionViewModel(IEventAggregator eventAggregator, IBuildProviderSettingsRepository buildProviderSettingsRepository)
        {
            if (eventAggregator == null)
            {
                throw new ArgumentNullException(nameof(eventAggregator));
            }

            if (buildProviderSettingsRepository == null)
            {
                throw new ArgumentNullException(nameof(buildProviderSettingsRepository));
            }

            _eventAggregator = eventAggregator;
            _buildProviderSettingsRepository = buildProviderSettingsRepository;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public void AddConnection()
        {
            _buildProviderSettingsRepository.Add(GetSettings());

            ViewConnections();
        }

        public void ViewConnections()
        {
            var eventMessage = new NavigationEvent(typeof(ConnectionsViewModel));

            _eventAggregator.PublishOnUIThread(eventMessage);
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns>The settings.</returns>
        protected abstract BuildProviderSettings GetSettings();
    }
}
