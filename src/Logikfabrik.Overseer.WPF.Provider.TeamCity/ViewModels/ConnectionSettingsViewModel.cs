// <copyright file="ConnectionSettingsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.ViewModels
{
    using System.Linq;
    using FluentValidation;
    using Validators;

    /// <summary>
    /// The <see cref="ConnectionSettingsViewModel" /> class.
    /// </summary>
    public class ConnectionSettingsViewModel : WPF.ViewModels.ConnectionSettingsViewModel<ConnectionSettings>
    {
        private string _url;
        private AuthenticationType _authenticationType;
        private string _username;
        private string _password;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsViewModel" /> class.
        /// </summary>
        public ConnectionSettingsViewModel()
        {
            Validator = new ConnectionSettingsViewModelValidator();
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url
        {
            get
            {
                return _url;
            }

            set
            {
                _url = value;
                NotifyOfPropertyChange(() => Url);

                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the authentication type.
        /// </summary>
        /// <value>
        /// The authentication type.
        /// </value>
        public AuthenticationType AuthenticationType
        {
            get
            {
                return _authenticationType;
            }

            set
            {
                _authenticationType = value;
                NotifyOfPropertyChange(() => AuthenticationType);

                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
                NotifyOfPropertyChange(() => Username);

                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);

                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <value>
        /// The validator.
        /// </value>
        public override IValidator Validator { get; }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns>The settings.</returns>
        public override ConnectionSettings GetSettings()
        {
            return new ConnectionSettings
            {
                Name = Name,
                Url = Url,
                AuthenticationType = AuthenticationType,
                Username = Username,
                Password = Password,
                ProjectsToMonitor = ProjectsToMonitor.Where(project => project.Monitor).Select(project => project.Id).ToArray()
            };
        }

        /// <summary>
        /// Updates the settings.
        /// </summary>
        /// <param name="current">The current settings.</param>
        public override void UpdateSettings(ConnectionSettings current)
        {
            current.Name = Name;
            current.Url = Url;
            current.AuthenticationType = AuthenticationType;
            current.Username = Username;
            current.Password = Password;
            current.ProjectsToMonitor = ProjectsToMonitor.Where(project => project.Monitor).Select(project => project.Id).ToArray();
        }
    }
}
