﻿// <copyright file="IMouseManager.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System.Windows.Input;

    /// <summary>
    /// The <see cref="IMouseManager" /> interface.
    /// </summary>
    public interface IMouseManager
    {
        event PreProcessInputEventHandler PreProcessInput;
    }
}
