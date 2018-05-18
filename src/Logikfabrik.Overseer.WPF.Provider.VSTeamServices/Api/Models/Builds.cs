// <copyright file="Builds.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Api.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="Builds" /> class.
    /// </summary>
    public class Builds
    {
        /// <summary>
        /// Gets or sets the builds count.
        /// </summary>
        /// <value>
        /// The builds count.
        /// </value>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public IEnumerable<Build> Value { get; set; }
    }
}
