// <copyright file="BuildStatus.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.Codeship.Api.Models
{
    /// <summary>
    /// The <see cref="BuildStatus" /> enumeration.
    /// </summary>
    public enum BuildStatus
    {
        /// <summary>
        /// The build failed.
        /// </summary>
        Failed,

        /// <summary>
        /// The build is started.
        /// </summary>
        Started,

        /// <summary>
        /// The build is recovered.
        /// </summary>
        Recovered,

        /// <summary>
        /// The build succeeded.
        /// </summary>
        Success,

        /// <summary>
        /// The build is being tested.
        /// </summary>
        Testing,

        /// <summary>
        /// The build is waiting.
        /// </summary>
        Waiting
    }
}
