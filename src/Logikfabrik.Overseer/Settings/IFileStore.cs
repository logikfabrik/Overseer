// <copyright file="IFileStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;

    /// <summary>
    /// The <see cref="IFileStore" /> interface.
    /// </summary>
    public interface IFileStore : IDisposable
    {
        string Read();

        void Write(string contents);
    }
}