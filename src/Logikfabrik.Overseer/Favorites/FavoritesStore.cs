// <copyright file="FavoritesStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Favorites
{
    using EnsureThat;
    using IO;

    /// <summary>
    /// The <see cref="FavoritesStore" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class FavoritesStore : IFavoritesStore
    {
        private readonly IFavoritesSerializer _serializer;
        private readonly IFileStore _fileStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritesStore" /> class.
        /// </summary>
        /// <param name="serializer">The serializer.</param>
        /// <param name="fileStore">The file store.</param>
        public FavoritesStore(IFavoritesSerializer serializer, IFileStore fileStore)
        {
            Ensure.That(serializer).IsNotNull();
            Ensure.That(fileStore).IsNotNull();

            _serializer = serializer;
            _fileStore = fileStore;
        }

        /// <inheritdoc />
        public Favorite[] Load()
        {
            var xml = _fileStore.Read();

            return string.IsNullOrWhiteSpace(xml)
                ? new Favorite[] { }
                : _serializer.Deserialize(xml);
        }

        /// <inheritdoc />
        public void Save(Favorite[] favorites)
        {
            Ensure.That(favorites).IsNotNull();

            var xml = _serializer.Serialize(favorites);

            _fileStore.Write(xml);
        }
    }
}
