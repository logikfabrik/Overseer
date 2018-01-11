// <copyright file="BuildProvidersWizardStepViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using EnsureThat;
using Logikfabrik.Overseer.WPF.Navigation;
using Logikfabrik.Overseer.WPF.ViewModels;

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    /// <summary>
    /// The <see cref="BuildProvidersWizardStepViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class BuildProvidersWizardStepViewModel : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private IBuildProviderViewModel _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProvidersWizardStepViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="providerViewModels">The provider view models.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public BuildProvidersWizardStepViewModel(IPlatformProvider platformProvider, IEventAggregator eventAggregator, IEnumerable<IBuildProviderViewModel> providerViewModels)
            : base(platformProvider)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(providerViewModels).IsNotNull();

            _eventAggregator = eventAggregator;

            var providers = providerViewModels.ToArray();

            Providers = providers;
            Provider = providers.FirstOrDefault();
        }

        /// <summary>
        /// Gets the providers.
        /// </summary>
        /// <value>
        /// The providers.
        /// </value>
        public IEnumerable<IBuildProviderViewModel> Providers { get; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        public IBuildProviderViewModel Provider
        {
            get
            {
                return _provider;
            }

            set
            {
                _provider = value;
                NotifyOfPropertyChange(() => Provider);
            }
        }

        public void AddConnection()
        {
            _provider?.AddConnection();
        }

        public void SkipStep()
        {
            var message = new NavigationMessage(typeof(FinishWizardStepViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
