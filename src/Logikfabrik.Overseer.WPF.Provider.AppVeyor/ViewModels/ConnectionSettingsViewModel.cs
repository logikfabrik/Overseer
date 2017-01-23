// <copyright file="ConnectionSettingsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.ViewModels
{
    using System.Linq;
    using FluentValidation;
    using Validators;
    using WPF.ViewModels.Factories;

    /// <summary>
    /// The <see cref="ConnectionSettingsViewModel" /> class.
    /// </summary>
    public class ConnectionSettingsViewModel : WPF.ViewModels.ConnectionSettingsViewModel<ConnectionSettings>
    {
        private string _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsViewModel" /> class.
        /// </summary>
        /// <param name="projectToMonitorFactory">The project to monitor factory.</param>
        public ConnectionSettingsViewModel(IProjectToMonitorViewModelFactory projectToMonitorFactory)
            : base(projectToMonitorFactory)
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
                Token = Token,
                ProjectsToMonitor = ProjectsToMonitor.Where(project => project.Monitor).Select(project => project.Id).ToArray()
            };
        }

        /// <summary>
        /// Gets the updated settings.
        /// </summary>
        /// <param name="current">The current settings.</param>
        /// <returns>The updated settings.</returns>
        public override ConnectionSettings GetSettings(ConnectionSettings current)
        {
            current.Name = Name;
            current.Token = Token;
            current.ProjectsToMonitor = ProjectsToMonitor.Where(project => project.Monitor).Select(project => project.Id).ToArray();

            return current;
        }
    }
}