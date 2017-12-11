// <copyright file="UriUtility.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.Codeship
{
    using System;

    /// <summary>
    /// The <see cref="UriUtility" /> class.
    /// </summary>
    public static class UriUtility
    {
        /// <summary>
        /// Gets the base URI.
        /// </summary>
#pragma warning disable S1075 // URIs should not be hardcoded
        public static Uri BaseUri => new Uri("https://api.codeship.com/v2/");
#pragma warning restore S1075 // URIs should not be hardcoded
    }
}
