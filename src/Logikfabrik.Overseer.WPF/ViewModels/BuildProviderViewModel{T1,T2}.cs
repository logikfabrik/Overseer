// <copyright file="BuildProviderViewModel{T1,T2}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="BuildProviderViewModel{T1,T2}" /> class.
    /// </summary>
    public class BuildProviderViewModel<T1, T2> : PropertyChangedBase, IBuildProviderViewModel
        where T1 : ConnectionSettings
        where T2 : AddConnectionViewModel<T1>
    {
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderViewModel{T1,T2}" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="providerName">The provider name.</param>
        public BuildProviderViewModel(IEventAggregator eventAggregator, string providerName)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(providerName).IsNotNullOrWhiteSpace();

            _eventAggregator = eventAggregator;
            ProviderName = providerName;
        }

        /// <summary>
        /// Gets the provider name.
        /// </summary>
        /// <value>
        /// The provider name.
        /// </value>
        public string ProviderName { get; }

        /// <summary>
        /// Navigates to the view to add connection.
        /// </summary>
        public void AddConnection()
        {
            var message = new NavigationMessage(typeof(T2));

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}