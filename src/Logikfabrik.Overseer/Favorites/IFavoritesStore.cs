// <copyright file="IFavoritesStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Favorites
{
    /// <summary>
    /// The <see cref="IFavoritesStore" /> interface.
    /// </summary>
    public interface IFavoritesStore
    {
        /// <summary>
        /// Loads the favorites.
        /// </summary>
        /// <returns>
        /// The favorites.
        /// </returns>
        Favorite[] Load();

        /// <summary>
        /// Saves the specified favorites.
        /// </summary>
        /// <param name="favorites">The favorites.</param>
        void Save(Favorite[] favorites);
    }
}