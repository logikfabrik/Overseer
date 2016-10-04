﻿// <copyright file="AddConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.ViewModels
{
    using System.ComponentModel;
    using System.Linq;
    using Caliburn.Micro;
    using FluentValidation;
    using Settings;
    using Validators;

    /// <summary>
    /// The <see cref="AddConnectionViewModel" /> class.
    /// </summary>
    public class AddConnectionViewModel : WPF.ViewModels.AddConnectionViewModel, IDataErrorInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildProviderSettingsRepository">The build provider settings repository.</param>
        public AddConnectionViewModel(IEventAggregator eventAggregator, IBuildProviderSettingsRepository buildProviderSettingsRepository)
            : base(eventAggregator, buildProviderSettingsRepository)
        {
            Url = "https://";
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; set; }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                var validator = new AddConnectionViewModelValidator();

                var results = validator.Validate(this, columnName);

                return results.IsValid
                    ? null
                    : results.Errors.First().ErrorMessage;
            }
        }

        /// <summary>
        /// Gets the build provider settings.
        /// </summary>
        /// <returns>
        /// The build provider settings.
        /// </returns>
        protected override BuildProviderSettings GetBuildProviderSettings()
        {
            return new BuildProviderSettings
            {
                Name = ConnectionName,
                BuildProviderTypeName = typeof(BuildProvider).AssemblyQualifiedName,
                Settings = new[]
                {
                    new Setting { Name = "Url", Value = Url },
                    new Setting { Name = "Token", Value = Token }
                }
            };
        }
    }
}
