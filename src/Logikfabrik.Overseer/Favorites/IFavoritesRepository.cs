﻿// <copyright file="IFavoritesRepository.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Favorites
{
    using System;
    using Notification;

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
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        void Remove(Guid settingsId, string projectId);

        /// <summary>
        /// Removes the favorites with the specified identifier.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        void Remove(Guid settingsId);

        /// <summary>
        /// Determines whether a favorite for the specified settings identifier and project identifier exists.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <returns><c>true</c> if a favorite exists; otherwise, <c>false</c>.</returns>
        bool Exists(Guid settingsId, string projectId);
    }
}