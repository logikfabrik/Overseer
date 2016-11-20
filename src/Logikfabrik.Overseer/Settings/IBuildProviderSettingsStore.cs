// <copyright file="IBuildProviderSettingsStore.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System.Threading.Tasks;

    /// <summary>
    /// The <see cref="IBuildProviderSettingsStore" /> interface.
    /// </summary>
    public interface IBuildProviderSettingsStore
    {
        Task<BuildProviderSettings[]> LoadAsync();

        Task SaveAsync(BuildProviderSettings[] settings);
    }
}