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
    // ReSharper disable once InheritdocConsiderUsage
    public class ConnectionSettingsViewModel : ConnectionSettingsViewModel<ConnectionSettings>
    {
        private const string Version = "10.0";

        private string _url;
        private AuthenticationType _authenticationType;
        private string _username;
        private string _password;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsViewModel" /> class.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public ConnectionSettingsViewModel()
        {
            Validator = new ConnectionSettingsViewModelValidator();
            Url = string.Concat(Uri.UriSchemeHttps, Uri.SchemeDelimiter);
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

                // Trigger revalidation of base URI.
                NotifyOfPropertyChange(() => BaseUri);
                NotifyOfPropertyChange(() => ShowBaseUri);

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

                return UriUtility.TryGetBaseUri(Url, Version, AuthenticationType, out baseUri) ? baseUri.ToString() : null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether to show the base URI.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the base URI should be shown; otherwise, <c>false</c>.
        /// </value>
        public bool ShowBaseUri => BaseUri != null;

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

                // Trigger revalidation of base URI.
                NotifyOfPropertyChange(() => BaseUri);
                NotifyOfPropertyChange(() => ShowBaseUri);

                // Trigger revalidation of username and password.
                NotifyOfPropertyChange(() => ShowUsernameAndPassword);
                NotifyOfPropertyChange(() => Username);
                NotifyOfPropertyChange(() => Password);

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
        /// Gets a value indicating whether to show the username and password.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the username and password should be shown; otherwise, <c>false</c>.
        /// </value>
        public bool ShowUsernameAndPassword => AuthenticationType == AuthenticationType.HttpAuth;

        /// <inheritdoc />
        public override ConnectionSettings GetSettings()
        {
            var projects = TrackedProjects?.Projects?.Where(project => project.Track).ToArray() ?? new TrackedProjectViewModel[] { };

            return new ConnectionSettings
            {
                Name = Name,
                Url = Url,
                AuthenticationType = AuthenticationType,
                Version = Version,
                Username = Username,
                Password = Password,
                TrackedProjects = projects.Select(project => project.Id).ToArray(),
                BuildsPerProject = BuildsPerProject
            };
        }

        /// <inheritdoc />
        public override void UpdateSettings(ConnectionSettings current)
        {
            current.Name = Name;
            current.Url = Url;
            current.AuthenticationType = AuthenticationType;
            current.Version = Version;
            current.Username = Username;
            current.Password = Password;
            current.TrackedProjects = TrackedProjects.Projects.Where(project => project.Track).Select(project => project.Id).ToArray();
            current.BuildsPerProject = BuildsPerProject;
        }
    }
}
