// <copyright file="BuildOutcome.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI.Api.Models
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// The <see cref="BuildOutcome" /> enumeration.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BuildOutcome
    {
        /// <summary>
        /// The build was canceled.
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
        /// The build failed.
        /// </summary>
        Failed,

        /// <summary>
        /// The build had no tests.
        /// </summary>
        [EnumMember(Value = "no_tests")]
        NoTests,

        /// <summary>
        /// The build was a success.
        /// </summary>
        Success
    }
}
