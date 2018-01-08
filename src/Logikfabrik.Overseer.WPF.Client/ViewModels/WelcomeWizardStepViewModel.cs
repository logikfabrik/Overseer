// <copyright file="WelcomeWizardStepViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using Navigation;

    /// <summary>
    /// The <see cref="WelcomeWizardStepViewModel" /> class.
    /// </summary>
    public class WelcomeWizardStepViewModel : IWizardStepViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="WelcomeWizardStepViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public WelcomeWizardStepViewModel(IEventAggregator eventAggregator)
        {
            Ensure.That(eventAggregator).IsNotNull();

            _eventAggregator = eventAggregator;
        }

        public void Next()
        {
            var message = new NavigationMessage(typeof(PassPhraseWizardStepViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
