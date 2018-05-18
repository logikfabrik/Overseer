// <copyright file="BuildLifecycle.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI.Api.Models
{
    /// <summary>
    /// The <see cref="BuildLifecycle" /> enumeration.
    /// </summary>
    public enum BuildLifecycle
    {
        /// <summary>
        /// The build finished.
        /// </summary>
        Finished,

        /// <summary>
        /// The build is queued.
        /// </summary>
        Queued,

        /// <summary>
        /// The build is scheduled.
        /// </summary>
        Scheduled,

        /// <summary>
        /// The build was not run.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        Not_run,

        /// <summary>
        /// The build is not running.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        Not_running,

        /// <summary>
        /// The build is running.
        /// </summary>
        Running
    }
}
