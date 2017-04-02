// <copyright file="EditSettingsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using Validators;

    /// <summary>
    /// The <see cref="EditSettingsViewModel" /> class. View model for editing application wide settings.
    /// </summary>
    public class EditSettingsViewModel : ViewModel
    {
        private readonly EditSettingsViewModelValidator _validator;
        private readonly IEventAggregator _eventAggregator;
        private readonly AppSettings _appSettings;
        private string _proxyUrl;
        private string _proxyUsername;
        private string _proxyPassword;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditSettingsViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="appSettings">The application settings.</param>
        public EditSettingsViewModel(IEventAggregator eventAggregator, AppSettings appSettings)
        {
            Ensure.That(appSettings).IsNotNull();

            _validator = new EditSettingsViewModelValidator();
            _eventAggregator = eventAggregator;
            _appSettings = appSettings;
            _proxyUrl = appSettings.ProxyUrl;
            _proxyUsername = appSettings.ProxyUsername;
            _proxyPassword = appSettings.ProxyPassword;
        }

        /// <summary>
        /// Gets the view name.
        /// </summary>
        /// <value>
        /// The view name.
        /// </value>
        public override string ViewName { get; } = "Edit settings";

        /// <summary>
        /// Gets or sets the proxy URL.
        /// </summary>
        /// <value>
        /// The proxy URL.
        /// </value>
        public string ProxyUrl
        {
            get
            {
                return _proxyUrl;
            }

            set
            {
                _proxyUrl = value;
                NotifyOfPropertyChange(() => ProxyUrl);
            }
        }

        /// <summary>
        /// Gets or sets the proxy username.
        /// </summary>
        /// <value>
        /// The proxy username.
        /// </value>
        public string ProxyUsername
        {
            get
            {
                return _proxyUsername;
            }

            set
            {
                _proxyUsername = value;
                NotifyOfPropertyChange(() => ProxyUsername);
            }
        }

        /// <summary>
        /// Gets or sets the proxy password.
        /// </summary>
        /// <value>
        /// The proxy password.
        /// </value>
        public string ProxyPassword
        {
            get
            {
                return _proxyPassword;
            }

            set
            {
                _proxyPassword = value;
                NotifyOfPropertyChange(() => ProxyPassword);
            }
        }

        /// <summary>
        /// Edit the settings.
        /// </summary>
        public void EditSettings()
        {
            if (!_validator.Validate(this).IsValid)
            {
                return;
            }

            _appSettings.ProxyUrl = _proxyUrl;
            _appSettings.ProxyUsername = _proxyUsername;
            _appSettings.ProxyPassword = _proxyPassword;

            _appSettings.Save();

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
    }
}