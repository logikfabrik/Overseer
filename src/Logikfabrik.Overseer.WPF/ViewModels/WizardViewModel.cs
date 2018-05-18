// <copyright file="WizardViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;
    using JetBrains.Annotations;
    using Passphrase;

    /// <summary>
    /// The <see cref="WizardViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
#pragma warning disable S110 // Inheritance tree of classes should not be too deep
    public sealed class WizardViewModel : Conductor<IWizardViewModel>
#pragma warning restore S110 // Inheritance tree of classes should not be too deep
    {
        private readonly IPassphraseRepository _passphraseRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WizardViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="passphraseRepository">The passphrase repository.</param>
        /// <param name="welcomeWizardStepViewModel">The welcome wizard step view model.</param>
        [UsedImplicitly]

        // ReSharper disable once InheritdocConsiderUsage
        public WizardViewModel(IEventAggregator eventAggregator, IPassphraseRepository passphraseRepository, WizardStartViewModel welcomeWizardStepViewModel)
            : base(eventAggregator)
        {
            Ensure.That(passphraseRepository).IsNotNull();
            Ensure.That(welcomeWizardStepViewModel).IsNotNull();

            _passphraseRepository = passphraseRepository;

            DisplayName = "Overseer";

            ActivateItem(welcomeWizardStepViewModel);
        }

        /// <inheritdoc />
        public override void CanClose(Action<bool> callback)
        {
            callback(_passphraseRepository.HasHash);
        }
    }
}