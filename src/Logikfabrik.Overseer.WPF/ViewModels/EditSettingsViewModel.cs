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
        private string _cultureName;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditSettingsViewModel" /> class.
        /// </summary>
        /// <param name="appSettingsFactory">The app settings factory.</param>
        public EditSettingsViewModel(IAppSettingsFactory appSettingsFactory)
        {
            Ensure.That(appSettingsFactory).IsNotNull();

            _validator = new EditSettingsViewModelValidator();

            var appSettings = appSettingsFactory.Create();

            _appSettings = appSettings;
            _interval = appSettings.Interval;
            _cultureName = appSettings.CultureName;
            DisplayName = Properties.Resources.EditSettings_View;
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
                NotifyOfPropertyChange(() => IsValid);
            }
        }

        /// <summary>
        /// Gets or sets the culture name.
        /// </summary>
        /// <value>
        /// The culture name.
        /// </value>
        public string CultureName
        {
            get
            {
                return _cultureName;
            }

            set
            {
                _cultureName = value;
                NotifyOfPropertyChange(() => CultureName);
                NotifyOfPropertyChange(() => IsValid);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid => _validator.Validate(this).IsValid;

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
        /// Edit the settings.
        /// </summary>
        public void Edit()
        {
            if (!IsValid)
            {
                return;
            }

            _appSettings.Interval = _interval;
            _appSettings.CultureName = _cultureName;

            _appSettings.Save();

            LanguageConfigurator.Configure(_appSettings);
        }
    }
}