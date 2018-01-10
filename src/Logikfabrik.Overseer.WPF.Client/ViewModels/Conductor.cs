// <copyright file="Conductor.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;
    using Navigation;
    using Overseer.Extensions;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="Conductor" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public abstract class Conductor : Conductor<IViewModel>.Collection.OneActive, IHandle<NavigationMessage>, IDisposable
    {
        private IEventAggregator _eventAggregator;
        private Navigator<IViewModel> _navigator;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Conductor" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        // ReSharper disable once InheritdocConsiderUsage
        protected Conductor(IEventAggregator eventAggregator)
        {
            Ensure.That(eventAggregator).IsNotNull();

            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _navigator = new Navigator<IViewModel>(this);
        }

        /// <inheritdoc />
        public void Handle(NavigationMessage message)
        {
            this.ThrowIfDisposed(_isDisposed);

            _navigator.Navigate(message);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _eventAggregator?.Unsubscribe(this);
            _eventAggregator = null;
            _navigator = null;

            _isDisposed = true;
        }
    }
}