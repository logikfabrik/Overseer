// <copyright file="IAppSettingsFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    /// <summary>
    /// The <see cref="IAppSettingsFactory" /> interface.
    /// </summary>
    public interface IAppSettingsFactory
    {
        /// <summary>
        /// Creates an <see cref="AppSettings" /> instance.
        /// </summary>
        /// <returns>An <see cref="AppSettings" /> instance.</returns>
        AppSettings Create();
    }
}
