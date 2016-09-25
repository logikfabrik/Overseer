// <copyright file="Commit.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Api.Models
{
    using System;

    /// <summary>
    /// The <see cref="Commit" /> class.
    /// </summary>
    public class Commit
    {
        /// <summary>
        /// Gets or sets the commit identifier.
        /// </summary>
        /// <value>
        /// The commit identifier.
        /// </value>
        public string CommitId { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public UserRef Author { get; set; }

        /// <summary>
        /// Gets or sets the committer.
        /// </summary>
        /// <value>
        /// The committer.
        /// </value>
        public UserRef Committer { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public Uri Url { get; set; }

        /// <summary>
        /// Gets or sets the remote URL.
        /// </summary>
        /// <value>
        /// The remote URL.
        /// </value>
        public Uri RemoteUrl { get; set; }
    }
}
