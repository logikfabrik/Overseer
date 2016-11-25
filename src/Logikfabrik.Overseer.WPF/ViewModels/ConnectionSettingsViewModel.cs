// <copyright file="ConnectionSettingsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.ComponentModel;
    using System.Linq;
    using Caliburn.Micro;
    using FluentValidation;

    /// <summary>
    /// The <see cref="ConnectionSettingsViewModel" /> class.
    /// </summary>
    public abstract class ConnectionSettingsViewModel : PropertyChangedBase, IDataErrorInfo
    {
        private string _name;

        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <value>
        /// The validator.
        /// </value>
        public abstract IValidator Validator { get; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
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
                var result = Validator.Validate(this);

                if (result.IsValid)
                {
                    return null;
                }

                var error = result.Errors.FirstOrDefault(e => e.PropertyName == name);

                return error?.ErrorMessage;
            }
        }
    }
}
