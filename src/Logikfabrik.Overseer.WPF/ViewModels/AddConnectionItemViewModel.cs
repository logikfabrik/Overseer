// <copyright file="AddConnectionItemViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

using System;
using System.ComponentModel;

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="AddConnectionItemViewModel" /> class.
    /// </summary>
    public class AddConnectionItemViewModel : IItemViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionItemViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public AddConnectionItemViewModel(IEventAggregator eventAggregator)
        {
            Ensure.That(eventAggregator).IsNotNull();

            _eventAggregator = eventAggregator;
        }

        /// <summary>
        /// Add connection.
        /// </summary>
        public void Add()
        {
            var message = new NavigationMessage(typeof(BuildProvidersViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }

        public string DisplayName { get; set; }
        public void Activate()
        {
            throw new NotImplementedException();
        }

        public bool IsActive { get; }
        public event EventHandler<ActivationEventArgs> Activated;
        public void Deactivate(bool close)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<DeactivationEventArgs> AttemptingDeactivation;
        public event EventHandler<DeactivationEventArgs> Deactivated;
        public void TryClose(bool? dialogResult = null)
        {
            throw new NotImplementedException();
        }

        public void CanClose(Action<bool> callback)
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyOfPropertyChange(string propertyName)
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public bool IsNotifying { get; set; }
    }
}
