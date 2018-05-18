// <copyright file="BuildStatus.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.Api.Models
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
        /// The build succeeded.
        /// </summary>
        Success,

        /// <summary>
        /// The build failed.
        /// </summary>
        Failed,

        /// <summary>
        /// The build is canceled.
        /// </summary>
        [EnumMember(Value = "cancelled")]
        Canceled,

        /// <summary>
        /// The build is queued.
        /// </summary>
        Queued,

        /// <summary>
        /// The build is running.
        /// </summary>
        Running
    }
}
