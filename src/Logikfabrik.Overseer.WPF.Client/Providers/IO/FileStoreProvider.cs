// <copyright file="FileStoreProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Providers.IO
{
    using System;
    using System.IO;
    using EnsureThat;
    using Ninject.Activation;
    using Overseer.IO;

    /// <summary>
    /// The <see cref="FileStoreProvider" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public abstract class FileStoreProvider : Provider<IFileStore>
    {
        private readonly IFileSystem _fileSystem;
        private readonly string _fileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileStoreProvider" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="fileName">The file name.</param>
        protected FileStoreProvider(IFileSystem fileSystem, string fileName)
        {
            Ensure.That(fileSystem).IsNotNull();
            Ensure.That(fileName).IsNotNullOrWhiteSpace();

            _fileSystem = fileSystem;
            _fileName = fileName;
        }

        /// <inheritdoc />
        protected override IFileStore CreateInstance(IContext context)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Overseer", _fileName);

            return new FileStore(_fileSystem, path);
        }
    }
}
