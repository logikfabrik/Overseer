// <copyright file="WelcomeWizardStepViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using Logikfabrik.Overseer.WPF.ViewModels;
    using Navigation;

    /// <summary>
    /// The <see cref="WelcomeWizardStepViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class WelcomeWizardStepViewModel : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="WelcomeWizardStepViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public WelcomeWizardStepViewModel(IPlatformProvider platformProvider, IEventAggregator eventAggregator) 
            : base(platformProvider)
        {
            Ensure.That(eventAggregator).IsNotNull();

            _eventAggregator = eventAggregator;
        }

        public void NextStep()
        {
            var message = new NavigationMessage(typeof(PassPhraseWizardStepViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
