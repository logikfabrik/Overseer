// <copyright file="AddConnectionViewErrorLocalizer.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Localization
{
    using System;
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

            var innerException = exception.InnerException();

            var httpException = innerException as HttpException;

            if (httpException == null)
            {
                return innerException is SocketException
                    ? Properties.Resources.AddConnection_Error_Network
                    : Properties.Resources.AddConnection_Error_Standard;
            }

            if ((int)httpException.StatusCode >= 400 && (int)httpException.StatusCode <= 499)
            {
                return Properties.Resources.AddConnection_Error_HTTP4xx;
            }

            if ((int)httpException.StatusCode >= 500 && (int)httpException.StatusCode <= 599)
            {
                return Properties.Resources.AddConnection_Error_HTTP5xx;
            }

            return Properties.Resources.AddConnection_Error_Standard;
        }
    }
}
