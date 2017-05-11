// <copyright file="BuildStatus.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI.Api.Models
{
    /// <summary>
    /// The <see cref="BuildStatus" /> enumeration.
    /// </summary>
    public enum BuildStatus
    {
        Retried,

        Canceled,

        // ReSharper disable once InconsistentNaming
        Infrastructure_fail,

        Timedout,

        // ReSharper disable once InconsistentNaming
        Not_run,

        Running,

        Failed,

        Queued,

        Scheduled,

        // ReSharper disable once InconsistentNaming
        Not_running,

        // ReSharper disable once InconsistentNaming
        No_tests,

        Fixed,

        Success
    }
}
