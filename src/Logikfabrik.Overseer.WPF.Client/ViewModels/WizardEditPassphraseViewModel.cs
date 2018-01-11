// <copyright file="WizardEditPassphraseViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using System.ComponentModel;
    using System.Linq;
    using Caliburn.Micro;
    using EnsureThat;
    using Navigation;
    using Settings;
    using Validators;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="WizardEditPassphraseViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class WizardEditPassphraseViewModel : ViewModel, IDataErrorInfo
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IConnectionSettingsEncrypter _encrypter;
        private readonly PassphraseWizardStepViewModelValidator _validator;
        private string _passphrase;

        /// <summary>
        /// Initializes a new instance of the <see cref="WizardEditPassphraseViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="encrypter">The encrypter.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public WizardEditPassphraseViewModel(IPlatformProvider platformProvider, IEventAggregator eventAggregator, IConnectionSettingsEncrypter encrypter)
            : base(platformProvider)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(encrypter).IsNotNull();

            _eventAggregator = eventAggregator;
            _encrypter = encrypter;
            _validator = new PassphraseWizardStepViewModelValidator();
        }

        /// <summary>
        /// Gets or sets the passphrase.
        /// </summary>
        /// <value>
        /// The passphrase.
        /// </value>
        public string Passphrase
        {
            get
            {
                return _passphrase;
            }

            set
            {
                _passphrase = value;
                NotifyOfPropertyChange(() => Passphrase);
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

        public void NextStep()
        {
            if (!IsValid)
            {
                return;
            }

            _encrypter.SetPassphrase(_passphrase);

            var message = new NavigationMessage(typeof(WizardNewConnectionViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
