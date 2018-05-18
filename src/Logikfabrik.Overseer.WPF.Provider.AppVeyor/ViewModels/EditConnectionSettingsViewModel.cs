// <copyright file="EditConnectionSettingsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.ViewModels
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
        private string _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionSettingsViewModel" /> class.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public EditConnectionSettingsViewModel()
        {
            Validator = new EditConnectionSettingsViewModelValidator();
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
            var projects = TrackedProjects?.Projects?.Where(project => project.Track).ToArray() ?? new EditTrackedProjectViewModel[] { };

            return new ConnectionSettings
            {
                Name = Name,
                Token = Token,
                TrackedProjects = projects.Select(project => project.Id).ToArray(),
                BuildsPerProject = BuildsPerProject
            };
        }

        /// <inheritdoc />
        public override void UpdateSettings(ConnectionSettings current)
        {
            current.Name = Name;
            current.Token = Token;
            current.TrackedProjects = TrackedProjects.Projects.Where(project => project.Track).Select(project => project.Id).ToArray();
            current.BuildsPerProject = BuildsPerProject;
        }
    }
}