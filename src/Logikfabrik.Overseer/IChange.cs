// <copyright file="IChange.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;

    /// <summary>
    /// The <see cref="IChange" /> interface.
    /// </summary>
    public interface IChange
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; }

        /// <summary>
        /// Gets the changed date.
        /// </summary>
        /// <value>
        /// The changed date.
        /// </value>
        DateTime? Changed { get; }

        /// <summary>
        /// Gets the name of whoever made the last change.
        /// </summary>
        /// <value>
        /// The name of whoever made the last change.
        /// </value>
        string ChangedBy { get; }

        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        string Comment { get; }
    }
}