// <copyright file="TriggerType.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Api.Models
{
    /// <summary>
    /// The <see cref="TriggerType" /> enumeration.
    /// </summary>
    public enum TriggerType
    {
        /// <summary>
        /// Build was triggered.
        /// </summary>
        Unknown,

        /// <summary>
        /// Build was triggered by VCS.
        /// </summary>
        Vcs,

        /// <summary>
        /// Build was triggered by user.
        /// </summary>
        User,

        /// <summary>
        /// Build was triggered by build type.
        /// </summary>
        BuildType,

        /// <summary>
        /// Build was triggered by a restart.
        /// </summary>
        Restarted,

        /// <summary>
        /// Build was triggered by schedule.
        /// </summary>
        Schedule,

        SnapshotDependency,

        Retry
    }
}
