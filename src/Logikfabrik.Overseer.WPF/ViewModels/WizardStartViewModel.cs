// <copyright file="WizardStartViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using Navigation.Factories;

    /// <summary>
    /// The <see cref="WizardStartViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class WizardStartViewModel : IWizardViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationMessageFactory<WizardEditPassphraseViewModel> _navigationMessageFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="WizardStartViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="navigationMessageFactory">The navigation message factory.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public WizardStartViewModel(IEventAggregator eventAggregator, INavigationMessageFactory<WizardEditPassphraseViewModel> navigationMessageFactory)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(navigationMessageFactory).IsNotNull();

            _eventAggregator = eventAggregator;
            _navigationMessageFactory = navigationMessageFactory;
        }

        public void NextStep()
        {
            var message = _navigationMessageFactory.Create();

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
