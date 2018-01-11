// <copyright file="WizardNewConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Caliburn.Micro;
    using EnsureThat;
    using Navigation;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="WizardNewConnectionViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class WizardNewConnectionViewModel : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private IBuildProviderViewModel _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="WizardNewConnectionViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="providerViewModels">The provider view models.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public WizardNewConnectionViewModel(IPlatformProvider platformProvider, IEventAggregator eventAggregator, IEnumerable<IBuildProviderViewModel> providerViewModels)
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
            var message = new NavigationMessage(typeof(WizardFinishViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
