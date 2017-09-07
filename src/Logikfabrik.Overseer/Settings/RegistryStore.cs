// <copyright file="RegistryStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;
    using EnsureThat;
    using Microsoft.Win32;
    using Extensions;

    /// <summary>
    /// The <see cref="RegistryStore" /> class.
    /// </summary>
    public class RegistryStore : IRegistryStore, IDisposable
    {
        private RegistryKey _key;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryStore" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public RegistryStore(string path)
        {
            Ensure.That(path).IsNotNullOrWhiteSpace();

            _key = Registry.CurrentUser.CreateSubKey(path);
        }

        /// <summary>
        /// Writes the specified key.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void Write(string name, string value)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(name).IsNotNullOrWhiteSpace();
            Ensure.That(value).IsNotNullOrWhiteSpace();

            _key.SetValue(name, value);
        }

        /// <summary>
        /// Reads the specified key.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The value.</returns>
        public string Read(string name)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(name).IsNotNullOrWhiteSpace();

            return _key.GetValue(name) as string;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
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
                if (_key != null)
                {
                    _key.Dispose();
                    _key = null;
                }
            }

            _isDisposed = true;
        }
    }
}
