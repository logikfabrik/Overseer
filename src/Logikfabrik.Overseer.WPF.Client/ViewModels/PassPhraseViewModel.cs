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
    // ReSharper disable once InheritdocConsiderUsage
    public class PassPhraseViewModel : ViewAware, IClose, IHaveDisplayName, IDataErrorInfo
    {
        private readonly IConnectionSettingsEncrypter _encrypter;
        private readonly PassPhraseViewModelValidator _validator;
        private string _passPhrase;

        /// <summary>
        /// Initializes a new instance of the <see cref="PassPhraseViewModel" /> class.
        /// </summary>
        /// <param name="encrypter">The encrypter.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public PassPhraseViewModel(IConnectionSettingsEncrypter encrypter)
        {
            Ensure.That(encrypter).IsNotNull();

            _encrypter = encrypter;
            _validator = new PassPhraseViewModelValidator();
        }

        /// <inheritdoc />
        public string DisplayName { get; set; } = Properties.Resources.PassPhrase_View;

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

        /// <inheritdoc />
        public string Error => null;

        /// <inheritdoc />
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
        /// Saves the pass phrase.
        /// </summary>
        public void Save()
        {
            if (!IsValid)
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

        /// <inheritdoc />
        public void TryClose(bool? dialogResult = null)
        {
            PlatformProvider.Current.GetViewCloseAction(this, Views.Values, dialogResult).OnUIThread();
        }
    }
}
