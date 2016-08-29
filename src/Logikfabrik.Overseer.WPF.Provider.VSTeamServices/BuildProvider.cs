// <copyright file="BuildProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="BuildProvider" /> class.
    /// </summary>
    /// <seealso cref="Overseer.BuildProvider" />
    public class BuildProvider : Overseer.BuildProvider
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public override string ProviderName { get; } = "VS Team Services";

        public override IEnumerable<IProject> GetProjects()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IBuild> GetBuilds(string projectId)
        {
            throw new NotImplementedException();
        }
    }
}
