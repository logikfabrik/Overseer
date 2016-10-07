// <copyright file="BuildProviderMetadata.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices
{
    /// <summary>
    /// The <see cref="BuildProviderMetadata" /> class.
    /// </summary>
    public class BuildProviderMetadata : IBuildProviderMetadata
    {
        /// <summary>
        /// Gets the provider name.
        /// </summary>
        /// <value>
        /// The provider name.
        /// </value>
        public string ProviderName { get; } = "Visual Studio Team Services";
    }
}
