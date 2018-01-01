// <copyright file="Repository.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// The <see cref="Repository" /> class.
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

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
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the GitHub language.
        /// </summary>
        /// <value>
        /// The GitHub language.
        /// </value>
        [JsonProperty(PropertyName = "github_language")]
        public string GitHubLanguage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Repository" /> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Repository" /> is private.
        /// </summary>
        /// <value>
        ///   <c>true</c> if private; otherwise, <c>false</c>.
        /// </value>
        public bool Private { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public Owner Owner { get; set; }

        /// <summary>
        /// Gets or sets the default branch.
        /// </summary>
        /// <value>
        /// The default branch.
        /// </value>
        [JsonProperty(PropertyName = "default_branch")]
        public Branch DefaultBranch { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Repository" /> is starred.
        /// </summary>
        /// <value>
        ///   <c>true</c> if starred; otherwise, <c>false</c>.
        /// </value>
        public bool Starred { get; set; }

        /// <summary>
        /// Gets or sets the current build.
        /// </summary>
        /// <value>
        /// The current build.
        /// </value>
        [JsonProperty(PropertyName = "current_build")]
        public Build CurrentBuild { get; set; }
    }
}
