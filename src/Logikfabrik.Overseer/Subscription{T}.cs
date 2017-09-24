// <copyright file="Subscription{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using EnsureThat;

    /// <summary>
    /// The <see cref="Subscription{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The notification type.</typeparam>
    internal class Subscription<T> : IDisposable
    {
        private HashSet<IObserver<T>> _observers;
        private IObserver<T> _observer;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Subscription{T}" /> class.
        /// </summary>
        /// <param name="observers">The observers.</param>
        /// <param name="observer">The observer.</param>
        internal Subscription(HashSet<IObserver<T>> observers, IObserver<T> observer)
        {
            Ensure.That(observers).IsNotNull();
            Ensure.That(observer).IsNotNull();

            _observers = observers;
            _observer = observer;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
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

            if (disposing)
            {
                if (_observers != null && _observer != null)
                {
                    _observers.Remove(_observer);
                }

                _observers = null;
                _observer = null;
            }

            _isDisposed = true;
        }
    }
}
