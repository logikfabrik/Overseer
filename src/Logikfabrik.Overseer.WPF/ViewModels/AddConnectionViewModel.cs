// <copyright file="AddConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="AddConnectionViewModel" /> class.
    /// </summary>
    public abstract class AddConnectionViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;
        private string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="eventAggregator" /> is <c>null</c>.</exception>
        protected AddConnectionViewModel(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null)
            {
                throw new ArgumentNullException(nameof(eventAggregator));
            }

            _eventAggregator = eventAggregator;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public void AddConnection()
        {
        }

        public void ViewConnections()
        {
            var eventMessage = new NavigationEvent(typeof(ConnectionsViewModel));

            _eventAggregator.PublishOnUIThread(eventMessage);
        }
    }
}
