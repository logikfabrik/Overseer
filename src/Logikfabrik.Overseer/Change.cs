// <copyright file="Change.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The <see cref="Change" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
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

        /// <inheritdoc />
        public string Id { get; }

        /// <inheritdoc />
        public string ShortId { get; }

        /// <inheritdoc />
        public DateTime? Changed { get; }

        /// <inheritdoc />
        public string ChangedBy { get; }

        /// <inheritdoc />
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
