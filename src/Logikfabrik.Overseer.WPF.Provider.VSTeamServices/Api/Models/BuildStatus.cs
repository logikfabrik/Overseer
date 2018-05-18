// <copyright file="BuildStatus.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Api.Models
{
    /// <summary>
    /// The <see cref="BuildStatus" /> enumeration.
    /// </summary>
    public enum BuildStatus
    {
        /// <summary>
        /// The build has no status.
        /// </summary>
        None = 0,

        /// <summary>
        /// The build is in progress.
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// The build has completed.
        /// </summary>
        Completed = 2,

        /// <summary>
        /// The build is cancelling.
        /// </summary>
        Cancelling = 4,

        /// <summary>
        /// The build is postponed.
        /// </summary>
        Postponed = 8,

        /// <summary>
        /// The build is not started.
        /// </summary>
        NotStarted = 32,

        All = 47
    }
}
