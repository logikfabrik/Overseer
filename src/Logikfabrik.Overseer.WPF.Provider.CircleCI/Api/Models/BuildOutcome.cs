// <copyright file="BuildOutcome.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI.Api.Models
{
    /// <summary>
    /// The <see cref="BuildOutcome" /> enumeration.
    /// </summary>
    public enum BuildOutcome
    {
        Canceled,

        // ReSharper disable once InconsistentNaming
        Infrastructure_fail,

        Timedout,

        Failed,

        // ReSharper disable once InconsistentNaming
        No_tests,

        Success
    }
}
