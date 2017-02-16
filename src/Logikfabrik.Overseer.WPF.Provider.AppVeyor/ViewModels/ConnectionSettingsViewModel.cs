﻿// <copyright file="ConnectionSettingsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.ViewModels
{
    using System.Linq;
    using FluentValidation;
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
            var projects = ProjectsToMonitor?.Projects?.Where(project => project.Monitor) ?? new ProjectToMonitorViewModel[] { };

            return new ConnectionSettings
            {
                Name = Name,
                Token = Token,
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
            current.Token = Token;
            current.ProjectsToMonitor = ProjectsToMonitor.Projects.Where(project => project.Monitor).Select(project => project.Id).ToArray();
        }
    }
}