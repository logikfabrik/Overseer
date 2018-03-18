// <copyright file="FavoriteIdentifier.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Favorites
{
    using System;

    /// <summary>
    /// The <see cref="FavoriteIdentifier" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class FavoriteIdentifier : Tuple<Guid, string>, IFavoriteIdentifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FavoriteIdentifier" /> class.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public FavoriteIdentifier(Guid settingsId, string projectId)
            : base(settingsId, projectId)
        {
        }
    }
}
