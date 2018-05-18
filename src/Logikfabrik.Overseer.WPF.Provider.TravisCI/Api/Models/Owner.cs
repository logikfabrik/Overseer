// <copyright file="Owner.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api.Models
{
    /// <summary>
    /// The <see cref="Owner" /> class.
    /// </summary>
    public class Owner
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user or organization GitHub login.
        /// </summary>
        /// <value>
        /// The user or organization GitHub login.
        /// </value>
        public string Login { get; set; }
    }
}
