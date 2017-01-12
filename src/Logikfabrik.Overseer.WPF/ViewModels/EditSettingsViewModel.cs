// <copyright file="EditSettingsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using EnsureThat;

    /// <summary>
    /// The <see cref="EditSettingsViewModel" /> class. View model for editing application wide settings.
    /// </summary>
    public class EditSettingsViewModel : ViewModel
    {
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditSettingsViewModel" /> class.
        /// </summary>
        /// <param name="appSettings">The application settings.</param>
        public EditSettingsViewModel(AppSettings appSettings)
        {
            Ensure.That(appSettings).IsNotNull();

            _appSettings = appSettings;

            // TODO: Input/output of settings. And validation.
        }

        /// <summary>
        /// Gets the view name.
        /// </summary>
        /// <value>
        /// The view name.
        /// </value>
        public override string ViewName { get; } = "Settings";
    }
}