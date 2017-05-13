﻿// <copyright file="BaseUriHelper.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI
{
    using System;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BaseUriHelper" /> class.
    /// </summary>
    public static class BaseUriHelper
    {
        /// <summary>
        /// Creates a base <see cref="Uri" />.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>A base URI.</returns>
        public static Uri GetBaseUri(string version)
        {
            Ensure.That(version).IsNotNullOrWhiteSpace();

            var builder = new UriBuilder("https://circleci.com/")
            {
                Path = $"api/{version}/"
            };

            return builder.Uri;
        }
    }
}
