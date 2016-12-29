// <copyright file="BuildProviderB.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Settings;

    public class BuildProviderB : BuildProvider<ConnectionSettingsB>
    {
        public BuildProviderB(ConnectionSettingsB settings)
            : base(settings)
        {
        }

        public override Task<IEnumerable<IProject>> GetProjectsAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<IBuild>> GetBuildsAsync(string projectId)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
        }
    }
}
