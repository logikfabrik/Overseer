﻿// <copyright file="IMouseManager.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System.Windows.Input;

    /// <summary>
    /// The <see cref="IMouseManager" /> interface.
    /// </summary>
    public interface IMouseManager
    {
        /// <summary>
        /// Occurs when the input item starts to be processed.
        /// </summary>
        event PreProcessInputEventHandler PreProcessInput;
    }
}
