// <copyright file="ConnectionSettingsA.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using System;
    using Overseer.Settings;

    public class ConnectionSettingsA : ConnectionSettings
    {
        public string SettingA { get; set; }

        public override Type ProviderType { get; } = typeof(object);
    }
}
