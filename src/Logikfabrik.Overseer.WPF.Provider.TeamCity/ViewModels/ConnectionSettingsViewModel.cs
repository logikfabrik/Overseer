// <copyright file="ConnectionSettingsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.ViewModels
{
    using System;
    using System.Linq;
    using Validators;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="ConnectionSettingsViewModel" /> class.
    /// </summary>
    public class ConnectionSettingsViewModel : ConnectionSettingsViewModel<ConnectionSettings>
    {
        private string _url;
        private AuthenticationType _authenticationType;
        private string _username;
        private string _password;
        private string _version;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsViewModel" /> class.
        /// </summary>
        public ConnectionSettingsViewModel()
        {
            Validator = new ConnectionSettingsViewModelValidator();
            Url = string.Concat(Uri.UriSchemeHttps, Uri.SchemeDelimiter);
            Version = "10.0";
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
                NotifyOfPropertyChange(() => IsValid);
                NotifyOfPropertyChange(() => BaseUri);

                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets the base URI.
        /// </summary>
        /// <value>
        /// The base URI.
        /// </value>
        public string BaseUri
        {
            get
            {
                Uri baseUri;

                return BaseUriHelper.TryGetBaseUri(Url, Version, AuthenticationType, out baseUri) ? baseUri.ToString() : null;
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
                NotifyOfPropertyChange(() => IsValid);
                NotifyOfPropertyChange(() => BaseUri);

                // Trigger revalidation of username and password.
                NotifyOfPropertyChange(() => Username);
                NotifyOfPropertyChange(() => Password);

                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version
        {
            get
            {
                return _version;
            }

            set
            {
                _version = value;
                NotifyOfPropertyChange(() => Version);
                NotifyOfPropertyChange(() => IsValid);
                NotifyOfPropertyChange(() => BaseUri);

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
                NotifyOfPropertyChange(() => IsValid);

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
                NotifyOfPropertyChange(() => IsValid);

                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns>The settings.</returns>
        public override ConnectionSettings GetSettings()
        {
            var projects = ProjectsToMonitor?.Projects?.Where(project => project.Monitor).ToArray() ?? new ProjectToMonitorViewModel[] { };

            return new ConnectionSettings
            {
                Name = Name,
                Url = Url,
                AuthenticationType = AuthenticationType,
                Version = Version,
                Username = Username,
                Password = Password,
                ProjectsToMonitor = projects.Select(project => project.Id).ToArray()
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
            current.Version = Version;
            current.Username = Username;
            current.Password = Password;
            current.ProjectsToMonitor = ProjectsToMonitor.Projects.Where(project => project.Monitor).Select(project => project.Id).ToArray();
        }
    }
}
