// <copyright file="BuildTypes.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Api.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="BuildTypes" /> class.
    /// </summary>
    public class BuildTypes
    {
        /// <summary>
        /// Gets or sets the build types.
        /// </summary>
        /// <value>
        /// The build types.
        /// </value>
        public IEnumerable<BuildType> BuildType { get; set; }
    }
}
