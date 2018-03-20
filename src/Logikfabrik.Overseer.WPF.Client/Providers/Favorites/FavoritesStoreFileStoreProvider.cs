// <copyright file="FavoritesStoreFileStoreProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Providers.Favorites
{
    using IO;
    using Overseer.IO;

    /// <summary>
    /// The <see cref="FavoritesStoreFileStoreProvider" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class FavoritesStoreFileStoreProvider : FileStoreProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritesStoreFileStoreProvider" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public FavoritesStoreFileStoreProvider(IFileSystem fileSystem)
            : base(fileSystem, "Favorites.xml")
        {
        }
    }
}
