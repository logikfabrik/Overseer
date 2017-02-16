// <copyright file="AuthenticationType.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity
{
    /// <summary>
    /// The <see cref="AuthenticationType" /> enumeration.
    /// </summary>
    public enum AuthenticationType
    {
        /// <summary>
        /// Basic authentication.
        /// </summary>
        HttpAuth = 0,

        /// <summary>
        /// Guest authentication.
        /// </summary>
        GuestAuth = 1
    }
}
