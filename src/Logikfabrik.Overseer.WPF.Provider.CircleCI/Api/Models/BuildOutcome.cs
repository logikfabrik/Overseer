// <copyright file="BuildOutcome.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
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
        Canceled,

        [EnumMember(Value = "infrastructure_fail")]
        InfrastructureFail,

        [EnumMember(Value = "timedout")]
        TimedOut,

        Failed,

        [EnumMember(Value = "no_tests")]
        NoTests,

        Success
    }
}
