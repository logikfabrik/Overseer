// <copyright file="BuildProviderSettingsRepository.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="BuildProviderSettingsRepository" /> class.
    /// </summary>
    public class BuildProviderSettingsRepository
    {
        // TODO: Read/write from file.
        private readonly IDictionary<string, BuildProviderSettings> _settings = new Dictionary<string, BuildProviderSettings>();

        public void Add(BuildProviderSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            
        }

        public BuildProviderSettings Get(string name, Type providerType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BuildProviderSettings> Get()
        {
            throw new NotImplementedException();
        }
    }
}
