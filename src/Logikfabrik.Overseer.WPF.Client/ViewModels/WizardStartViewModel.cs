// <copyright file="WizardStartViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using Navigation;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="WizardStartViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class WizardStartViewModel : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="WizardStartViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public WizardStartViewModel(IPlatformProvider platformProvider, IEventAggregator eventAggregator)
            : base(platformProvider)
        {
            Ensure.That(eventAggregator).IsNotNull();

            _eventAggregator = eventAggregator;
        }

        public void NextStep()
        {
            var message = new NavigationMessage(typeof(WizardEditPassphraseViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
