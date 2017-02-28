// <copyright file="ICacheable.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Caching
{
    /// <summary>
    /// The <see cref="ICacheable" /> interface.
    /// </summary>
    public interface ICacheable
    {
        /// <summary>
        /// Gets the cache key for this <see cref="ICacheable" /> instance.
        /// </summary>
        /// <returns>The cache key.</returns>
        string GetCacheKey();
    }
}
