// <copyright file="StubBuild.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;

    /// <summary>
    /// The <see cref="StubBuild" /> class.
    /// </summary>
    public class StubBuild : IBuild
    {
        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the branch.
        /// </summary>
        /// <value>
        /// The branch.
        /// </value>
        public string Branch { get; set; }

        /// <summary>
        /// Gets or sets the started date.
        /// </summary>
        /// <value>
        /// The started date.
        /// </value>
        public DateTime? Started { get; set; }

        /// <summary>
        /// Gets or sets the finished date.
        /// </summary>
        /// <value>
        /// The finished date.
        /// </value>
        public DateTime? Finished { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public BuildStatus? Status { get; set; }
    }
}
