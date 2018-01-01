// <copyright file="HttpException.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Extensions
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Runtime.Serialization;

    /// <summary>
    /// The <see cref="HttpException" /> class.
    /// </summary>
    [Serializable]

    // ReSharper disable once InheritdocConsiderUsage
    public sealed class HttpException : HttpRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException" /> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public HttpException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; }

        /// <inheritdoc />
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(StatusCode), StatusCode);
        }
    }
}
