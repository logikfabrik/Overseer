// <copyright file="StartWizardViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="StartWizardViewModel" /> class.
    /// </summary>
#pragma warning disable S110 // Inheritance tree of classes should not be too deep

    // ReSharper disable once InheritdocConsiderUsage
    public sealed class StartWizardViewModel : Conductor<IWizardStepViewModel>
#pragma warning restore S110 // Inheritance tree of classes should not be too deep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartWizardViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="welcomeWizardStepViewModel">The welcome wizard step view model.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public StartWizardViewModel(IEventAggregator eventAggregator, WelcomeWizardStepViewModel welcomeWizardStepViewModel)
            : base(eventAggregator)
        {
            Ensure.That(welcomeWizardStepViewModel).IsNotNull();

            DisplayName = "Welcome";

            ActivateItem(welcomeWizardStepViewModel);
        }
    }
}