// <copyright file="BuildStatus.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Api.Models
{
    /// <summary>
    /// The <see cref="BuildStatus" /> enumeration.
    /// </summary>
    public enum BuildStatus
    {
        /// <summary>
        /// The build has no known status.
        /// </summary>
        Unknown,

        /// <summary>
        /// The build succeeded.
        /// </summary>
        Success,

        /// <summary>
        /// The build failed.
        /// </summary>
        Failure,

        /// <summary>
        /// The build is errored.
        /// </summary>
        Error
    }
}
