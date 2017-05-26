// <copyright file="PassPhraseViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using Caliburn.Micro;
    using EnsureThat;
    using Settings;
    using Validators;

    /// <summary>
    /// The <see cref="PassPhraseViewModel" /> class. View model for setting pass phrase.
    /// </summary>
    public class PassPhraseViewModel : ViewAware, IClose, IHaveDisplayName, IDataErrorInfo
    {
        private readonly IConnectionSettingsEncrypter _encrypter;
        private readonly PassPhraseViewModelValidator _validator;
        private string _passPhrase;

        public PassPhraseViewModel(IConnectionSettingsEncrypter encrypter)
        {
            Ensure.That(encrypter).IsNotNull();

            _encrypter = encrypter;
            _validator = new PassPhraseViewModelValidator();
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; } = "Pass phrase";

        /// <summary>
        /// Gets or sets the pass phrase.
        /// </summary>
        /// <value>
        /// The pass phrase.
        /// </value>
        public string PassPhrase
        {
            get
            {
                return _passPhrase;
            }

            set
            {
                _passPhrase = value;
                NotifyOfPropertyChange(() => PassPhrase);
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
        /// Save the pass phrase.
        /// </summary>
        public void Save()
        {
            if (!_validator.Validate(this).IsValid)
            {
                return;
            }

            _encrypter.SetPassPhrase(_passPhrase);

            TryClose(true);
        }

        public void CanClose(Action<bool> callback)
        {
            callback(_encrypter.HasPassPhrase);
        }

        public void TryClose(bool? dialogResult = null)
        {
            PlatformProvider.Current.GetViewCloseAction(this, Views.Values, dialogResult).OnUIThread();
        }
    }
}
