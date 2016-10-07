// <copyright file="IBuildProviderMetadata.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    /// <summary>
    /// The <see cref="IBuildProviderMetadata" /> interface.
    /// </summary>
    public interface IBuildProviderMetadata
    {
        /// <summary>
        /// Gets the provider name.
        /// </summary>
        /// <value>
        /// The provider name.
        /// </value>
        string ProviderName { get; }
    }
}
