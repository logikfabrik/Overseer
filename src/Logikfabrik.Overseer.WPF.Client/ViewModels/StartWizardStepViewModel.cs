// <copyright file="StartWizardStepViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

using Caliburn.Micro;
using EnsureThat;
using Logikfabrik.Overseer.WPF.Navigation;
using Logikfabrik.Overseer.WPF.ViewModels;

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    /// <summary>
    /// The <see cref="StartWizardStepViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class StartWizardStepViewModel : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartWizardStepViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public StartWizardStepViewModel(IPlatformProvider platformProvider, IEventAggregator eventAggregator)
            : base(platformProvider)
        {
            Ensure.That(eventAggregator).IsNotNull();

            _eventAggregator = eventAggregator;
        }

        public void NextStep()
        {
            var message = new NavigationMessage(typeof(PassphraseWizardStepViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
