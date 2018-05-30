// <copyright file="HttpException.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Extensions
{
    using System;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using EnsureThat;

    /// <summary>
    /// The <see cref="HttpException" /> class.
    /// </summary>
    [Serializable]

    // ReSharper disable once InheritdocConsiderUsage
    public class HttpException : Exception
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
        /// Initializes a new instance of the <see cref="HttpException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]

        // ReSharper disable once InheritdocConsiderUsage
        protected HttpException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            StatusCode = (HttpStatusCode)info.GetValue(nameof(StatusCode), typeof(HttpStatusCode));
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
            Ensure.That(info).IsNotNull();

            info.AddValue(nameof(StatusCode), StatusCode);

            base.GetObjectData(info, context);
        }
    }
}
