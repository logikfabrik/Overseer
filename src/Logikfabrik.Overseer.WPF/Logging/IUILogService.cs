// <copyright file="IUILogService.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Logging
{
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="IUILogService" /> interface.
    /// </summary>
#pragma warning disable S101 // Types should be named in camel case

    // ReSharper disable once InconsistentNaming
    // ReSharper disable once InheritdocConsiderUsage
    public interface IUILogService : ILog
#pragma warning restore S101 // Types should be named in camel case
    {
    }
}
