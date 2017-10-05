// <copyright file="FileStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;
    using System.IO;
    using System.Threading;
    using EnsureThat;
    using Extensions;

    /// <summary>
    /// The <see cref="FileStore" /> class.
    /// </summary>
    public class FileStore : IFileStore, IDisposable
    {
        private readonly IFileSystem _fileSystem;
        private readonly string _path;
        private ManualResetEventSlim _resetEvent;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileStore" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public FileStore(IFileSystem fileSystem, string path)
        {
            Ensure.That(fileSystem).IsNotNull();
            Ensure.That(path).IsNotNullOrWhiteSpace();

            _fileSystem = fileSystem;
            _path = path;
            _resetEvent = new ManualResetEventSlim(true);
        }

        /// <summary>
        /// Reads the file text.
        /// </summary>
        /// <returns>
        /// The file text.
        /// </returns>
        public string Read()
        {
            this.ThrowIfDisposed(_isDisposed);

            _resetEvent.Wait();

            try
            {
                return _fileSystem.FileExists(_path) ? _fileSystem.ReadFileText(_path) : null;
            }
            finally
            {
                _resetEvent.Set();
            }
        }

        /// <summary>
        /// Writes the specified file text to the file.
        /// </summary>
        /// <param name="text">The file text.</param>
        public void Write(string text)
        {
            this.ThrowIfDisposed(_isDisposed);

            _resetEvent.Wait();

            try
            {
                _fileSystem.CreateDirectory(Path.GetDirectoryName(_path));
                _fileSystem.WriteFileText(_path, text);
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
                if (_resetEvent != null)
                {
                    _resetEvent.Dispose();
                    _resetEvent = null;
                }
            }

            _isDisposed = true;
        }
    }
}