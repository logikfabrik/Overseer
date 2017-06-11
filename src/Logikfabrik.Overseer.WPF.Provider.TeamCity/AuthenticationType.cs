// <copyright file="AuthenticationType.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity
{
    using System.ComponentModel;

    /// <summary>
    /// The <see cref="AuthenticationType" /> enumeration.
    /// </summary>
    public enum AuthenticationType
    {
        /// <summary>
        /// Basic authentication.
        /// </summary>
        [Description("Username and password")]
        HttpAuth = 0,

        /// <summary>
        /// Guest authentication.
        /// </summary>
        [Description("Guest")]
        GuestAuth = 1
    }
}
