// <copyright file="Conductor{T1,T2}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;
    using Navigation;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="Conductor{T1,T2}" /> class.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    // ReSharper disable once InheritdocConsiderUsage
    public abstract class Conductor<T1, T2> : Conductor<T1>.Collection.OneActive, IHandle<INavigationMessage>, IDisposable
    where T1 : class
    {
        private IEventAggregator _eventAggregator;
        private Navigator<T1> _navigator;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Conductor{T1,T2}" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        // ReSharper disable once InheritdocConsiderUsage
        protected Conductor(IEventAggregator eventAggregator)
        {
            Ensure.That(eventAggregator).IsNotNull();

            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _navigator = new Navigator<T1>(this);
        }

        /// <inheritdoc />
        public virtual void Handle(INavigationMessage message)
        {
            this.ThrowIfDisposed(_isDisposed);

            if (!(message.Item is T2))
            {
                return;
            }

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