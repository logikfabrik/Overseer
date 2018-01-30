// <copyright file="WizardNewConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Caliburn.Micro;
    using EnsureThat;
    using Navigation.Factories;

    /// <summary>
    /// The <see cref="WizardNewConnectionViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class WizardNewConnectionViewModel : Conductor<IViewModel, IViewModel>, IWizardViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationMessageFactory<WizardFinishViewModel> _navigationMessageFactory;
        private IBuildProviderViewModel _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="WizardNewConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="navigationMessageFactory">The navigation message factory.</param>
        /// <param name="providerViewModels">The provider view models.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public WizardNewConnectionViewModel(IEventAggregator eventAggregator, INavigationMessageFactory<WizardFinishViewModel> navigationMessageFactory, IEnumerable<IBuildProviderViewModel> providerViewModels, ConnectionsViewModel connectionsViewModel)
            : base(eventAggregator)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(navigationMessageFactory).IsNotNull();
            Ensure.That(providerViewModels).IsNotNull();
            Ensure.That(connectionsViewModel).IsNotNull();

            _eventAggregator = eventAggregator;

            _navigationMessageFactory = navigationMessageFactory;

            var providers = providerViewModels.ToArray();

            Providers = providers;
            Provider = providers.FirstOrDefault();
            Connections = connectionsViewModel;
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

        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <value>
        /// The connections.
        /// </value>
        public ConnectionsViewModel Connections { get; }

        public void AddConnection()
        {
            _provider?.AddConnection();
        }

        public void NextStep()
        {
            var message = _navigationMessageFactory.Create();

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
