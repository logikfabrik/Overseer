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
        public static Uri GetBaseUri(this ConnectionSettings settings)
        {
            var builder = new UriBuilder
            {
                Host = settings.Server,
                Path = $"{settings.AuthenticationType}/app/rest/{settings.ApiVersion}/"
            };

            if (settings.Port.HasValue)
            {
                builder.Port = settings.Port.Value;
            }

            return builder.Uri;
        }
    }
}
