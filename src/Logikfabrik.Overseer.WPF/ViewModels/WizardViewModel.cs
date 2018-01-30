// <copyright file="WizardViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="WizardViewModel" /> class.
    /// </summary>
#pragma warning disable S110 // Inheritance tree of classes should not be too deep

    // ReSharper disable once InheritdocConsiderUsage
    public sealed class WizardViewModel : Conductor<IWizardViewModel, IWizardViewModel>
#pragma warning restore S110 // Inheritance tree of classes should not be too deep
    {
        private readonly IConnectionSettingsEncrypter _connectionSettingsEncrypter;

        /// <summary>
        /// Initializes a new instance of the <see cref="WizardViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="connectionSettingsEncrypter">The connection settings encrypter.</param>
        /// <param name="welcomeWizardStepViewModel">The welcome wizard step view model.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public WizardViewModel(IEventAggregator eventAggregator, IConnectionSettingsEncrypter connectionSettingsEncrypter, WizardStartViewModel welcomeWizardStepViewModel)
            : base(eventAggregator)
        {
            Ensure.That(connectionSettingsEncrypter).IsNotNull();
            Ensure.That(welcomeWizardStepViewModel).IsNotNull();

            _connectionSettingsEncrypter = connectionSettingsEncrypter;

            DisplayName = "Welcome";

            ActivateItem(welcomeWizardStepViewModel);
        }

        /// <inheritdoc />
        public override void CanClose(Action<bool> callback)
        {
            callback(_connectionSettingsEncrypter.HasPassphrase);
        }
    }
}