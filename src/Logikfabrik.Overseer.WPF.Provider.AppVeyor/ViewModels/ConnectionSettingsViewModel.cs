// <copyright file="ConnectionSettingsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.ViewModels
{
    using System.Linq;
    using Validators;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="ConnectionSettingsViewModel" /> class.
    /// </summary>
    public class ConnectionSettingsViewModel : ConnectionSettingsViewModel<ConnectionSettings>
    {
        private string _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsViewModel" /> class.
        /// </summary>
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

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns>The settings.</returns>
        public override ConnectionSettings GetSettings()
        {
            var projects = TrackedProjects?.Projects?.Where(project => project.Track).ToArray() ?? new TrackedProjectViewModel[] { };

            return new ConnectionSettings
            {
                Name = Name,
                Token = Token,
                TrackedProjects = projects.Select(project => project.Id).ToArray(),
                BuildsPerProject = BuildsPerProject
            };
        }

        /// <summary>
        /// Updates the settings.
        /// </summary>
        /// <param name="current">The current settings.</param>
        public override void UpdateSettings(ConnectionSettings current)
        {
            current.Name = Name;
            current.Token = Token;
            current.TrackedProjects = TrackedProjects.Projects.Where(project => project.Track).Select(project => project.Id).ToArray();
            current.BuildsPerProject = BuildsPerProject;
        }
    }
}