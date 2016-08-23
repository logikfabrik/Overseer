// <copyright file="IdentityRef.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Api.Models
{
    /// <summary>
    /// The <see cref="IdentityRef" /> class.
    /// </summary>
    public class IdentityRef
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the unique name.
        /// </summary>
        /// <value>
        /// The unique name.
        /// </value>
        public string UniqueName { get; set; }
    }
}
