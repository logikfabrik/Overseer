// <copyright file="BuildResult.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Api.Models
{
    /// <summary>
    /// The <see cref="BuildResult" /> enumeration.
    /// </summary>
    public enum BuildResult
    {
        /// <summary>
        /// The build has no result.
        /// </summary>
        None = 0,

        /// <summary>
        /// The build succeeded.
        /// </summary>
        Succeeded = 2,

        /// <summary>
        /// The build partially succeeded.
        /// </summary>
        PartiallySucceeded = 4,

        /// <summary>
        /// The build failed.
        /// </summary>
        Failed = 8,

        /// <summary>
        /// The build was canceled.
        /// </summary>
        Canceled = 32
    }
}
