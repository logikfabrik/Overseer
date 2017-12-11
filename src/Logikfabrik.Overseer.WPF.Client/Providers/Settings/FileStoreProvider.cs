// <copyright file="FileStoreProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Providers.Settings
{
    using System;
    using System.IO;
    using EnsureThat;
    using Ninject.Activation;
    using Overseer.Settings;

    /// <summary>
    /// The <see cref="FileStoreProvider" /> class.
    /// </summary>
    public class FileStoreProvider : Provider<IFileStore>
    {
        private readonly IFileSystem _fileSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileStoreProvider" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public FileStoreProvider(IFileSystem fileSystem)
        {
            Ensure.That(fileSystem).IsNotNull();

            _fileSystem = fileSystem;
        }

        /// <summary>
        /// Creates an instance within the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The created instance.
        /// </returns>
        protected override IFileStore CreateInstance(IContext context)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Overseer", "Providers.xml");

            return new FileStore(_fileSystem, path);
        }
    }
}
