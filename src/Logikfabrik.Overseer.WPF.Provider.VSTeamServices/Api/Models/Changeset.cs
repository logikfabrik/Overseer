// <copyright file="Changeset.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Api.Models
{
    using System;

    /// <summary>
    /// The <see cref="Changeset" /> class.
    /// </summary>
    public class Changeset
    {
        /// <summary>
        /// Gets or sets the changeset identifier.
        /// </summary>
        /// <value>
        /// The changeset identifier.
        /// </value>
        public string ChangesetId { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public Uri Url { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public IdentityRef Author { get; set; }

        /// <summary>
        /// Gets or sets who checked in the changeset.
        /// </summary>
        /// <value>
        /// Who checked in the changeset.
        /// </value>
        public IdentityRef CheckedInBy { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }
    }
}