// <copyright file="IFavoritesRepository.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Favorites
{
    using System;

    /// <summary>
    /// The <see cref="IFavoritesRepository" /> interface.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public interface IFavoritesRepository : IObservable<Notification<Favorite>[]>
    {
        /// <summary>
        /// Adds the specified favorite.
        /// </summary>
        /// <param name="favorite">The favorite.</param>
        void Add(Favorite favorite);

        /// <summary>
        /// Removes the favorite with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Remove(IFavoriteIdentifier id);
    }
}