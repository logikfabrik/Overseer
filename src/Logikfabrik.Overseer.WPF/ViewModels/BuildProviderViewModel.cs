// <copyright file="BuildProviderViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="BuildProviderViewModel" /> class.
    /// </summary>
    /// <seealso cref="PropertyChangedBase" />
    public abstract class BuildProviderViewModel : PropertyChangedBase
    {
        private readonly BuildProvider _buildProvider;
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildProvider">The build provider.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="eventAggregator" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="buildProvider" /> is <c>null</c>.</exception>
        protected BuildProviderViewModel(IEventAggregator eventAggregator, BuildProvider buildProvider)
        {
            if (eventAggregator == null)
            {
                throw new ArgumentNullException(nameof(eventAggregator));
            }

            if (buildProvider == null)
            {
                throw new ArgumentNullException(nameof(buildProvider));
            }

            _eventAggregator = eventAggregator;
            _buildProvider = buildProvider;
        }

        /// <summary>
        /// Gets the name of the provider.
        /// </summary>
        /// <value>
        /// The name of the provider.
        /// </value>
        public string ProviderName => _buildProvider.ProviderName;

        /// <summary>
        /// Gets the type of the view model to add connection.
        /// </summary>
        /// <value>
        /// The type of the view model to add connection.
        /// </value>
        protected abstract Type AddConnectionViewModel { get; }

        /// <summary>
        /// Navigates to the view to add connection.
        /// </summary>
        public void AddConnection()
        {
            var eventMessage = new NavigationEvent(AddConnectionViewModel);

            _eventAggregator.PublishOnUIThread(eventMessage);
        }
    }
}