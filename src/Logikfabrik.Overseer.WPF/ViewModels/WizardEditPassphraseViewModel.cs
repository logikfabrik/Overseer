// <copyright file="WizardEditPassphraseViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.ComponentModel;
    using System.Linq;
    using Caliburn.Micro;
    using EnsureThat;
    using JetBrains.Annotations;
    using Navigation;
    using Passphrase;
    using Validators;

    /// <summary>
    /// The <see cref="WizardEditPassphraseViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class WizardEditPassphraseViewModel : PropertyChangedBase, IDataErrorInfo, IWizardViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IPassphraseRepository _passphraseRepository;
        private readonly INavigationMessageFactory<WizardNewConnectionViewModel> _navigationMessageFactory;
        private readonly WizardEditPassphraseViewModelValidator _validator;
        private string _passphrase;

        /// <summary>
        /// Initializes a new instance of the <see cref="WizardEditPassphraseViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="passphraseRepository">The passphrase repository.</param>
        /// <param name="navigationMessageFactory">The navigation message factory.</param>
        [UsedImplicitly]

        // ReSharper disable once InheritdocConsiderUsage
        public WizardEditPassphraseViewModel(IEventAggregator eventAggregator, IPassphraseRepository passphraseRepository, INavigationMessageFactory<WizardNewConnectionViewModel> navigationMessageFactory)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(passphraseRepository).IsNotNull();
            Ensure.That(navigationMessageFactory).IsNotNull();

            _eventAggregator = eventAggregator;
            _passphraseRepository = passphraseRepository;
            _navigationMessageFactory = navigationMessageFactory;
            _validator = new WizardEditPassphraseViewModelValidator();
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

        /// <summary>
        /// Goes to the next wizard step.
        /// </summary>
        public void NextStep()
        {
            if (!IsValid)
            {
                return;
            }

            _passphraseRepository.WriteHash(PassphraseUtility.GetHash(_passphrase));

            var message = _navigationMessageFactory.Create();

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
