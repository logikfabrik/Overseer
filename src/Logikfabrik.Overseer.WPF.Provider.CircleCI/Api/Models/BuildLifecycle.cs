// <copyright file="BuildLifecycle.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI.Api.Models
{
    /// <summary>
    /// The <see cref="BuildLifecycle" /> enumeration.
    /// </summary>
    public enum BuildLifecycle
    {
        Finished,

        Queued,

        Scheduled,

        // ReSharper disable once InconsistentNaming
        Not_run,

        // ReSharper disable once InconsistentNaming
        Not_running,

        Running,
    }
}
