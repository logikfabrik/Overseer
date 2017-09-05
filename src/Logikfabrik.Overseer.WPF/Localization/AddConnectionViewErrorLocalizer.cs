// <copyright file="AddConnectionViewErrorLocalizer.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Localization
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="AddConnectionViewErrorLocalizer" /> class.
    /// </summary>
    public static class AddConnectionViewErrorLocalizer
    {
        /// <summary>
        /// Gets a localized UI message.
        /// </summary>
        /// <param name="exception">An exception.</param>
        /// <returns>A localized UI message.</returns>
        public static string Localize(Exception exception)
        {
            if (exception == null)
            {
                return null;
            }

            // TODO: This!

            var innerException = exception.InnerException();

            if (innerException is HttpException)
            {
                var ex = (HttpException)innerException;

                if ((int)ex.StatusCode >= 400 && (int)ex.StatusCode <= 499)
                {
                    return Properties.Resources.AddConnection_Error_HTTP4xx;
                }

                if ((int)ex.StatusCode >= 500 && (int)ex.StatusCode <= 599)
                {
                    return Properties.Resources.AddConnection_Error_HTTP5xx;
                }
            }

            if (innerException is SocketException)
            {
                var ex = (SocketException)innerException;
            }

            if (innerException is WebException)
            {
                var ex = (WebException)innerException;
            }

            return Properties.Resources.AddConnection_Error_Standard;
        }
    }
}
