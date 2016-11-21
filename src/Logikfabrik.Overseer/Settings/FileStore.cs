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
        private readonly EventWaitHandle _handle;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileStore" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public FileStore(string path)
        {
            Ensure.That(path).IsNotNullOrWhiteSpace();

            _path = path;
            _handle = new EventWaitHandle(true, EventResetMode.AutoReset, "b4908818-002e-42fb-a058-86ea4e47e36e");
        }

        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <returns>
        /// The contents.
        /// </returns>
        public string Read()
        {
            _handle.WaitOne();

            try
            {
                if (!File.Exists(_path))
                {
                    return null;
                }

                return File.ReadAllText(_path);
            }
            finally
            {
                _handle.Set();
            }
        }

        /// <summary>
        /// Writes the specified contents to the file.
        /// </summary>
        /// <param name="contents">The contents.</param>
        public void Write(string contents)
        {
            _handle.WaitOne();

            try
            {
                File.WriteAllText(_path, contents);
            }
            finally
            {
                _handle.Set();
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
                _handle.Dispose();
            }

            _isDisposed = true;
        }
    }
}
