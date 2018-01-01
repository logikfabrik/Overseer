// <copyright file="ConnectionSettingsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI.ViewModels
{
    using System.Linq;
    using Validators;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="ConnectionSettingsViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ConnectionSettingsViewModel : ConnectionSettingsViewModel<ConnectionSettings>
    {
        private const string Version = "v1.1";

        private string _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsViewModel" /> class.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public ConnectionSettingsViewModel()
        {
            Validator = new ConnectionSettingsViewModelValidator();
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

        /// <inheritdoc />
        public override ConnectionSettings GetSettings()
        {
            var projects = TrackedProjects?.Projects?.Where(project => project.Track).ToArray() ?? new TrackedProjectViewModel[] { };

            return new ConnectionSettings
            {
                Name = Name,
                Version = Version,
                Token = Token,
                TrackedProjects = projects.Select(project => project.Id).ToArray(),
                BuildsPerProject = BuildsPerProject
            };
        }

        /// <inheritdoc />
        public override void UpdateSettings(ConnectionSettings current)
        {
            current.Name = Name;
            current.Version = Version;
            current.Token = Token;
            current.TrackedProjects = TrackedProjects.Projects.Where(project => project.Track).Select(project => project.Id).ToArray();
            current.BuildsPerProject = BuildsPerProject;
        }
    }
}
