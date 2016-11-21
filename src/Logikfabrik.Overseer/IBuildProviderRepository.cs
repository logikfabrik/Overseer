// <copyright file="IBuildProviderRepository.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="IBuildProviderRepository" /> interface.
    /// </summary>
    public interface IBuildProviderRepository
    {
        /// <summary>
        /// Gets all the providers.
        /// </summary>
        /// <returns>
        /// All the providers.
        /// </returns>
        IEnumerable<IBuildProvider> GetAll();
    }
}
