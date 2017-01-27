// <copyright file="FileStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;
    using System.IO;
    using System.Threading;
    using EnsureThat;

    /// <summary>
    /// The <see cref="FileStore" /> class.
    /// </summary>
    public class FileStore : IFileStore
    {
        private readonly string _path;
        private readonly ManualResetEventSlim _resetEvent;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileStore" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public FileStore(string path)
        {
            Ensure.That(path).IsNotNullOrWhiteSpace();

            _path = path;
            _resetEvent = new ManualResetEventSlim(true);
        }

        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <returns>
        /// The contents.
        /// </returns>
        public string Read()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            _resetEvent.Wait();

            try
            {
                return !File.Exists(_path) ? null : File.ReadAllText(_path);
            }
            finally
            {
                _resetEvent.Set();
            }
        }

        /// <summary>
        /// Writes the specified contents to the file.
        /// </summary>
        /// <param name="contents">The contents.</param>
        public void Write(string contents)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            _resetEvent.Wait();

            try
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                Directory.CreateDirectory(Path.GetDirectoryName(_path));

                File.WriteAllText(_path, contents);
            }
            finally
            {
                _resetEvent.Set();
            }
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
                _resetEvent.Dispose();
            }

            _isDisposed = true;
        }
    }
}