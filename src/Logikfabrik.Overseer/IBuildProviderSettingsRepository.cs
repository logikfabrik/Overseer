// <copyright file="IBuildProviderSettingsRepository.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="IBuildProviderSettingsRepository" /> interface.
    /// </summary>
    public interface IBuildProviderSettingsRepository
    {
        void Add(BuildProviderSettings settings);

        IEnumerable<BuildProviderSettings> Get();
    }
}