// <copyright file="RegistryStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.IO.Registry
{
    using System;
    using EnsureThat;
    using Extensions;
    using Microsoft.Win32;

    /// <summary>
    /// The <see cref="RegistryStore" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
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

        /// <inheritdoc />
        public void Write(string name, string value)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(name).IsNotNullOrWhiteSpace();
            Ensure.That(value).IsNotNullOrWhiteSpace();

            _key.SetValue(name, value);
        }

        /// <inheritdoc />
        public string Read(string name)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(name).IsNotNullOrWhiteSpace();

            return _key.GetValue(name) as string;
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

            if (disposing && _key != null)
            {
                _key.Dispose();
                _key = null;
            }

            _isDisposed = true;
        }
    }
}
