// <copyright file="BuildState.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Api.Models
{
    /// <summary>
    /// The <see cref="BuildState" /> enumeration.
    /// </summary>
    public enum BuildState
    {
        /// <summary>
        /// The build has finished.
        /// </summary>
        Finished,

        /// <summary>
        /// The build is running.
        /// </summary>
        Running,

        /// <summary>
        /// The build is queued.
        /// </summary>
        Queued
    }
}