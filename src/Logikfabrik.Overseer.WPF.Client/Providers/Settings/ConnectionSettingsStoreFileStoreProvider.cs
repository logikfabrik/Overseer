﻿// <copyright file="ConnectionSettingsStoreFileStoreProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Providers.Settings
{
    using IO;
    using JetBrains.Annotations;
    using Overseer.IO;

    /// <summary>
    /// The <see cref="ConnectionSettingsStoreFileStoreProvider" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ConnectionSettingsStoreFileStoreProvider : FileStoreProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsStoreFileStoreProvider" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        [UsedImplicitly]

        // ReSharper disable once InheritdocConsiderUsage
        public ConnectionSettingsStoreFileStoreProvider(IFileSystem fileSystem)
            : base(fileSystem, "ConnectionSettings.xml")
        {
        }
    }
}
