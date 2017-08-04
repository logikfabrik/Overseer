// <copyright file="Change.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The <see cref="Change" /> class.
    /// </summary>
    public class Change : IChange
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Change" /> class.
        /// </summary>
        /// <param name="id">The identifier</param>
        /// <param name="changed">The changed date.</param>
        /// <param name="changedBy">The name of whoever made the change.</param>
        /// <param name="comment">The comment.</param>
        public Change(string id, DateTime? changed, string changedBy, string comment)
        {
            var shortId = GetShortId(id);

            Id = id;
            ShortId = shortId;
            Changed = changed;
            ChangedBy = changedBy;
            Comment = comment;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; }

        /// <summary>
        /// Gets the short identifier.
        /// </summary>
        /// <value>
        /// The short identifier.
        /// </value>
        public string ShortId { get; }

        /// <summary>
        /// Gets the changed date.
        /// </summary>
        /// <value>
        /// The changed date.
        /// </value>
        public DateTime? Changed { get; }

        /// <summary>
        /// Gets the name of whoever made the change.
        /// </summary>
        /// <value>
        /// The name of whoever made the change.
        /// </value>
        public string ChangedBy { get; }

        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; }

        private static string GetShortId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            return Regex.IsMatch(id, "^[a-fA-F0-9]{40}$") ? id.Substring(0, 8) : null;
        }
    }
}
