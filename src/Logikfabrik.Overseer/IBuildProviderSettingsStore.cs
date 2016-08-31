// <copyright file="IBuildProviderSettingsStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// The <see cref="IBuildProviderSettingsStore" /> interface.
    /// </summary>
    public interface IBuildProviderSettingsStore
    {
        void SaveAsync(IEnumerable<BuildProviderSettings> settings);

        Task<IEnumerable<BuildProviderSettings>> LoadAsync();
    }
}