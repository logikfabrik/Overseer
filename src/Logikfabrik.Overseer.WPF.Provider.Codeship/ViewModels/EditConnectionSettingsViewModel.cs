// <copyright file="EditConnectionSettingsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.Codeship.ViewModels
{
    using System.Linq;
    using Validators;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="EditConnectionSettingsViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class EditConnectionSettingsViewModel : EditConnectionSettingsViewModel<ConnectionSettings>
    {
        private string _username;
        private string _password;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionSettingsViewModel" /> class.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public EditConnectionSettingsViewModel()
        {
            Validator = new EditConnectionSettingsViewModelValidator();
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

        /// <inheritdoc />
        public override ConnectionSettings GetSettings()
        {
            var projects = TrackedProjects?.Projects?.Where(project => project.Track).ToArray() ?? new EditTrackedProjectViewModel[] { };

            return new ConnectionSettings
            {
                Name = Name,
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
            current.Username = Username;
            current.Password = Password;
            current.TrackedProjects = TrackedProjects.Projects.Where(project => project.Track).Select(project => project.Id).ToArray();
            current.BuildsPerProject = BuildsPerProject;
        }
    }
}