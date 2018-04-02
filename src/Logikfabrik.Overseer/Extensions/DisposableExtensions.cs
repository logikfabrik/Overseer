// <copyright file="DisposableExtensions.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Extensions
{
    using System;

    /// <summary>
    /// The <see cref="DisposableExtensions" /> class. Extensions for implementations of the <see cref="IDisposable" /> interface.
    /// </summary>
    public static class DisposableExtensions
    {
        /// <summary>
        /// Throws <see cref="ObjectDisposedException" /> if the specified <see cref="IDisposable" /> is disposed.
        /// </summary>
        /// <param name="disposable">The disposable.</param>
        /// <param name="isDisposed">Whether the specified <see cref="IDisposable" /> is disposed.</param>
        public static void ThrowIfDisposed(this IDisposable disposable, bool isDisposed)
        {
            if (!isDisposed)
            {
                return;
            }

            throw new ObjectDisposedException(disposable.GetType().FullName);
        }
    }
}
