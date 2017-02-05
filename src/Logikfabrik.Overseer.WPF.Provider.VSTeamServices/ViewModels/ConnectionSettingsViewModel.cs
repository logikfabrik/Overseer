﻿// <copyright file="ConnectionSettingsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.ViewModels
{
    using System.Linq;
    using FluentValidation;
    using Validators;

    /// <summary>
    /// The <see cref="ConnectionSettingsViewModel" /> class.
    /// </summary>
    public class ConnectionSettingsViewModel : WPF.ViewModels.ConnectionSettingsViewModel<ConnectionSettings>
    {
        private string _url;
        private string _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsViewModel" /> class.
        /// </summary>
        public ConnectionSettingsViewModel()
        {
            Validator = new ConnectionSettingsViewModelValidator();
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

                IsDirty = true;
            }
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
            return new ConnectionSettings
            {
                Name = Name,
                Url = Url,
                Token = Token,
                ProjectsToMonitor = ProjectsToMonitor.Where(project => project.Monitor).Select(project => project.Id).ToArray()
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
            current.Token = Token;
            current.ProjectsToMonitor = ProjectsToMonitor.Where(project => project.Monitor).Select(project => project.Id).ToArray();
        }
    }
}