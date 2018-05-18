// <copyright file="IDisplaySetting.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using System.Windows;

    /// <summary>
    /// The <see cref="IDisplaySetting" /> interface.
    /// </summary>
    public interface IDisplaySetting
    {
        /// <summary>
        /// Occurs if display settings are changed.
        /// </summary>
        event EventHandler DisplaySettingsChanged;

        /// <summary>
        /// Gets the work area.
        /// </summary>
        /// <value>
        /// The work area.
        /// </value>
        Rect WorkArea { get; }
    }
}