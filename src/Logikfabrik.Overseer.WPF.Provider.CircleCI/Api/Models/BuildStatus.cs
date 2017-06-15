// <copyright file="BuildStatus.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI.Api.Models
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// The <see cref="BuildStatus" /> enumeration.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BuildStatus
    {
        /// <summary>
        /// The build is retried.
        /// </summary>
        Retried,

        /// <summary>
        /// The build is canceled.
        /// </summary>
        Canceled,

        [EnumMember(Value = "infrastructure_fail")]
        InfrastructureFail,

        /// <summary>
        /// The build timed out.
        /// </summary>
        [EnumMember(Value = "timedout")]
        TimedOut,

        /// <summary>
        /// The build was not run.
        /// </summary>
        [EnumMember(Value = "not_run")]
        NotRun,

        /// <summary>
        /// The build is running.
        /// </summary>
        Running,

        /// <summary>
        /// The build failed.
        /// </summary>
        Failed,

        /// <summary>
        /// The build is queued.
        /// </summary>
        Queued,

        /// <summary>
        /// The build is scheduled.
        /// </summary>
        Scheduled,

        /// <summary>
        /// The build is not running.
        /// </summary>
        [EnumMember(Value = "not_running")]
        NotRunning,

        /// <summary>
        /// The build has no tests.
        /// </summary>
        [EnumMember(Value = "no_tests")]
        NoTests,

        Fixed,

        /// <summary>
        /// The build succeeded.
        /// </summary>
        Success
    }
}
