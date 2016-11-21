// <copyright file="BuildProviderViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildProviderViewModel" /> class.
    /// </summary>
    public abstract class BuildProviderViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        protected BuildProviderViewModel(IEventAggregator eventAggregator)
        {
            Ensure.That(eventAggregator).IsNotNull();

            _eventAggregator = eventAggregator;
        }

        /// <summary>
        /// Gets the provider name.
        /// </summary>
        /// <value>
        /// The provider name.
        /// </value>
        public abstract string ProviderName { get; }

        /// <summary>
        /// Gets the type of the view model to add a connection.
        /// </summary>
        /// <value>
        /// The type of the view model to add a connection.
        /// </value>
        protected abstract Type AddConnectionViewModelType { get; }

        /// <summary>
        /// Navigates to the view to add connection.
        /// </summary>
        public void AddConnection()
        {
            var message = new NavigationMessage(AddConnectionViewModelType);

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}