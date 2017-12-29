// <copyright file="ConnectionSettingsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.ViewModels
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
        private string _gitHubToken;
        private string _url;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsViewModel" /> class.
        /// </summary>
        public ConnectionSettingsViewModel()
        {
            Validator = new ConnectionSettingsViewModelValidator();
            Url = string.Concat(Uri.UriSchemeHttps, Uri.SchemeDelimiter);
        }

        /// <summary>
        /// Gets or sets the GitHub token.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string GitHubToken
        {
            get
            {
                return _gitHubToken;
            }

            set
            {
                _gitHubToken = value;
                NotifyOfPropertyChange(() => GitHubToken);
                NotifyOfPropertyChange(() => IsValid);

                IsDirty = true;
            }
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

                IsDirty = true;
            }
        }

        /// <inheritdoc/>
        public override ConnectionSettings GetSettings()
        {
            var projects = TrackedProjects?.Projects?.Where(project => project.Track).ToArray() ?? new TrackedProjectViewModel[] { };

            return new ConnectionSettings
            {
                Name = Name,
                GitHubToken = GitHubToken,
                Url = Url,
                TrackedProjects = projects.Select(project => project.Id).ToArray(),
                BuildsPerProject = BuildsPerProject
            };
        }

        /// <inheritdoc/>
        public override void UpdateSettings(ConnectionSettings current)
        {
            current.Name = Name;
            current.GitHubToken = GitHubToken;
            current.Url = Url;
            current.TrackedProjects = TrackedProjects.Projects.Where(project => project.Track).Select(project => project.Id).ToArray();
            current.BuildsPerProject = BuildsPerProject;
        }
    }
}