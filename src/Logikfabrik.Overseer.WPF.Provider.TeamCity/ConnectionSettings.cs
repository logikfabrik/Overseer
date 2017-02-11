// <copyright file="ConnectionSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The <see cref="ConnectionSettings" /> class.
    /// </summary>
    [XmlType("TeamCity")]
    public class ConnectionSettings : Settings.ConnectionSettings
    {
        /// <summary>
        /// Gets the provider type.
        /// </summary>
        /// <value>
        /// The provider type.
        /// </value>
        public override Type ProviderType { get; } = typeof(BuildProvider);

        public string Server { get; set; }

        public int? Port { get; set; }

        public AuthenticationType AuthenticationType { get; set; }

        public decimal ApiVersion { get; } = 10.0M;
    }
}
