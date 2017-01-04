// <copyright file="Subscription{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
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
        private readonly HashSet<IObserver<T>> _observers;
        private readonly IObserver<T> _observer;

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
            _observers.Remove(_observer);
        }
    }
}
