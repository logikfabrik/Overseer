// <copyright file="AddConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
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
        protected AddConnectionViewModel(IEventAggregator eventAggregator, IBuildProviderSettingsRepository buildProviderSettingsRepository)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildProviderSettingsRepository).IsNotNull();

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

        /// <summary>
        /// Add the connection.
        /// </summary>
        public void AddConnection()
        {
            _buildProviderSettingsRepository.Add(GetBuildProviderSettings());

            ViewConnections();
        }

        /// <summary>
        /// View the connections.
        /// </summary>
        public void ViewConnections()
        {
            var eventMessage = new NavigationEvent(typeof(ConnectionsViewModel));

            _eventAggregator.PublishOnUIThread(eventMessage);
        }

        /// <summary>
        /// Gets the build provider settings.
        /// </summary>
        /// <returns>The build provider settings.</returns>
        protected abstract BuildProviderSettings GetBuildProviderSettings();
    }
}
