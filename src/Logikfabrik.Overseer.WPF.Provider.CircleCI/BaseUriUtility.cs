// <copyright file="BaseUriUtility.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI
{
    using System;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BaseUriUtility" /> class.
    /// </summary>
    public static class BaseUriUtility
    {
        /// <summary>
        /// Creates a base <see cref="Uri" />.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>A base URI.</returns>
        public static Uri GetBaseUri(string version)
        {
            Ensure.That(version).IsNotNullOrWhiteSpace();

            var builder = new UriBuilder(UriUtility.BaseUri)
            {
                Path = $"api/{version}/"
            };

            return builder.Uri;
        }
    }
}
