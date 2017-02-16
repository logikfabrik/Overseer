// <copyright file="BuildStatus.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.Api.Models
{
    /// <summary>
    /// The <see cref="BuildStatus" /> enumeration.
    /// </summary>
    public enum BuildStatus
    {
        Success,

        Failed,

        Cancelled
    }
}
