// <copyright file="EditSettingsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.ComponentModel;
    using System.Linq;
    using EnsureThat;
    using Validators;

    /// <summary>
    /// The <see cref="EditSettingsViewModel" /> class. View model for editing application wide settings.
    /// </summary>
    public class EditSettingsViewModel : ViewModel, IDataErrorInfo
    {
        private readonly EditSettingsViewModelValidator _validator;
        private readonly AppSettings _appSettings;
        private int _interval;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditSettingsViewModel" /> class.
        /// </summary>
        /// <param name="appSettings">The application settings.</param>
        public EditSettingsViewModel(AppSettings appSettings)
        {
            Ensure.That(appSettings).IsNotNull();

            _validator = new EditSettingsViewModelValidator();
            _appSettings = appSettings;
            _interval = appSettings.Interval;
            DisplayName = "Settings";
        }

        /// <summary>
        /// Gets or sets the interval in seconds.
        /// </summary>
        /// <value>
        /// The interval in seconds.
        /// </value>
        public int Interval
        {
            get
            {
                return _interval;
            }

            set
            {
                _interval = value;
                NotifyOfPropertyChange(() => Interval);
            }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        public string Error => null;

        /// <summary>
        /// Gets the error message for the property with the specified name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <returns>
        /// The error message, if any.
        /// </returns>
        public string this[string name]
        {
            get
            {
                var result = _validator.Validate(this);

                if (result.IsValid)
                {
                    return null;
                }

                var error = result.Errors.FirstOrDefault(e => e.PropertyName == name);

                return error?.ErrorMessage;
            }
        }

        /// <summary>
        /// Save the settings.
        /// </summary>
        public void Save()
        {
            if (!_validator.Validate(this).IsValid)
            {
                return;
            }

            _appSettings.Interval = _interval;

            _appSettings.Save();
        }
    }
}