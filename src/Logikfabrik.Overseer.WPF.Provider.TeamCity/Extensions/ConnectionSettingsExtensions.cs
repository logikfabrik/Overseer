// <copyright file="ConnectionSettingsExtensions.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Extensions
{
    using System;

    /// <summary>
    /// The <see cref="ConnectionSettingsExtensions" /> class.
    /// </summary>
    public static class ConnectionSettingsExtensions
    {
        /// <summary>
        /// Gets the base URI.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The base URI.</returns>
        public static Uri GetBaseUri(this ConnectionSettings settings)
        {
            var builder = new UriBuilder(settings.Url)
            {
                Path = $"{settings.AuthenticationType}/app/rest/{settings.ApiVersion}/"
            };

            if (builder.Scheme != Uri.UriSchemeHttp && builder.Scheme != Uri.UriSchemeHttps)
            {
                throw new NotSupportedException($"Scheme of type '{builder.Scheme}' is not supported.");
            }

            return builder.Uri;
        }
    }
}
