// <copyright file="Project.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.Api.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="Project" /> class.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>
        /// The account identifier.
        /// </value>
        public int AccountId { get; set; }

        /// <summary>
        /// Gets or sets the account name.
        /// </summary>
        /// <value>
        /// The account name.
        /// </value>
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or sets the builds.
        /// </summary>
        /// <value>
        /// The builds.
        /// </value>
        public IEnumerable<Build> Builds { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        /// <value>
        /// The slug.
        /// </value>
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets the repository type.
        /// </summary>
        /// <value>
        /// The repository type.
        /// </value>
        public string RepositoryType { get; set; }

        /// <summary>
        /// Gets or sets the repository SCM.
        /// </summary>
        /// <value>
        /// The repository SCM.
        /// </value>
        public string RepositoryScm { get; set; }

        /// <summary>
        /// Gets or sets the repository name.
        /// </summary>
        /// <value>
        /// The repository name.
        /// </value>
        public string RepositoryName { get; set; }

        /// <summary>
        /// Gets or sets the repository branch.
        /// </summary>
        /// <value>
        /// The repository branch.
        /// </value>
        public string RepositoryBranch { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this project is private.
        /// </summary>
        /// <value>
        /// <c>true</c> if this project is private; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime? Created { get; set; }
    }
}
