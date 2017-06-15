// <copyright file="BaseUriUtility.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity
{
    using System;
    using System.Linq;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BaseUriUtility" /> class.
    /// </summary>
    public static class BaseUriUtility
    {
        /// <summary>
        /// Creates a base <see cref="Uri" />.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="version">The version.</param>
        /// <param name="authenticationType">The authentication type.</param>
        /// <returns>A base URI.</returns>
        public static Uri GetBaseUri(string url, string version, AuthenticationType authenticationType)
        {
            Ensure.That(url).IsNotNullOrWhiteSpace();
            Ensure.That(version).IsNotNullOrWhiteSpace();

            var builder = GetBuilder(url, version, authenticationType);

            if (!HasSupportedScheme(builder))
            {
                throw new NotSupportedException($"URI scheme of type '{builder.Scheme}' is not supported.");
            }

            return builder.Uri;
        }

        /// <summary>
        /// Creates a base <see cref="Uri" />.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="version">The version.</param>
        /// <param name="authenticationType">The authentication type.</param>
        /// <param name="result">A base URI.</param>
        /// <returns><c>true</c> if a base <see cref="Uri" /> could be created; otherwise, <c>false</c>.</returns>
        public static bool TryGetBaseUri(string url, string version, AuthenticationType authenticationType, out Uri result)
        {
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(version))
            {
                result = null;

                return false;
            }

            try
            {
                var builder = GetBuilder(url, version, authenticationType);

                if (builder == null || !HasSupportedScheme(builder))
                {
                    result = null;

                    return false;
                }

                result = builder.Uri;

                return true;
            }
            catch (Exception)
            {
                result = null;

                return false;
            }
        }

        private static UriBuilder GetBuilder(string url, string version, AuthenticationType authenticationType)
        {
            var authType = string.Empty;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (authenticationType)
            {
                case AuthenticationType.HttpAuth:
                    authType = "httpAuth";
                    break;
                case AuthenticationType.GuestAuth:
                    authType = "guestAuth";
                    break;
            }

            Uri result;

            if (!Uri.TryCreate(url, UriKind.Absolute, out result))
            {
                return null;
            }

            return new UriBuilder(result)
            {
                Path = $"{authType}/app/rest/{version}/"
            };
        }

        private static bool HasSupportedScheme(UriBuilder builder)
        {
            var supported = new[] { Uri.UriSchemeHttp, Uri.UriSchemeHttps };

            return supported.Contains(builder.Scheme);
        }
    }
}
