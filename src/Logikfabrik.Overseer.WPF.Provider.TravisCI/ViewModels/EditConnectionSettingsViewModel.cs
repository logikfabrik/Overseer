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
    /// The <see cref="EditConnectionSettingsViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class EditConnectionSettingsViewModel : EditConnectionSettingsViewModel<ConnectionSettings>
    {
        private string _token;
        private string _url;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionSettingsViewModel" /> class.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public EditConnectionSettingsViewModel()
        {
            Validator = new EditConnectionSettingsViewModelValidator();
            Url = string.Concat(Uri.UriSchemeHttps, Uri.SchemeDelimiter);
        }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token
        {
            get
            {
                return _token;
            }

            set
            {
                _token = value;
                NotifyOfPropertyChange(() => Token);
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

        /// <inheritdoc />
        public override ConnectionSettings GetSettings()
        {
            var projects = TrackedProjects?.Projects?.Where(project => project.Track).ToArray() ?? new EditTrackedProjectViewModel[] { };

            return new ConnectionSettings
            {
                Name = Name,
                Token = Token,
                Url = Url,
                TrackedProjects = projects.Select(project => project.Id).ToArray(),
                BuildsPerProject = BuildsPerProject
            };
        }

        /// <inheritdoc />
        public override void UpdateSettings(ConnectionSettings current)
        {
            current.Name = Name;
            current.Token = Token;
            current.Url = Url;
            current.TrackedProjects = TrackedProjects.Projects.Where(project => project.Track).Select(project => project.Id).ToArray();
            current.BuildsPerProject = BuildsPerProject;
        }
    }
}