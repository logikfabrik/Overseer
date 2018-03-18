// <copyright file="IFavoritesSerializer.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Favorites
{
    /// <summary>
    /// The <see cref="IFavoritesSerializer" /> interface.
    /// </summary>
    public interface IFavoritesSerializer
    {
        /// <summary>
        /// Deserializes the specified favorites.
        /// </summary>
        /// <param name="favorites">The favorites.</param>
        /// <returns>
        /// The deserialized favorites.
        /// </returns>
        Favorite[] Deserialize(string favorites);

        /// <summary>
        /// Serializes the specified favorites.
        /// </summary>
        /// <param name="favorites">The favorites.</param>
        /// <returns>
        /// The serialized favorites.
        /// </returns>
        string Serialize(Favorite[] favorites);
    }
}