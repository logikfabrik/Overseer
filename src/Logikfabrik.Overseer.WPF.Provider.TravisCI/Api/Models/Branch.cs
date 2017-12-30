// <copyright file="Branch.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// The <see cref="Branch" /> class.
    /// </summary>
    public class Branch
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        public Repository Repository { get; set; }

        [JsonProperty(PropertyName = "default_branch")]
        public bool DefaultBranch { get; set; }

        [JsonProperty(PropertyName = "exists_on_github")]
        public bool ExistsOnGitHub { get; set; }

        /// <summary>
        /// Gets or sets the last build.
        /// </summary>
        /// <value>
        /// The last build.
        /// </value>
        [JsonProperty(PropertyName = "last_build")]
        public Build LastBuild { get; set; }
    }
}