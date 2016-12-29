// <copyright file="ConnectionSettingsSerializerProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Providers.Settings
{
    using System;
    using System.Linq;
    using Ninject.Activation;
    using Overseer.Settings;

    /// <summary>
    /// The <see cref="ConnectionSettingsSerializerProvider" /> class.
    /// </summary>
    public class ConnectionSettingsSerializerProvider : Provider<ConnectionSettingsSerializer>
    {
        /// <summary>
        /// Creates an instance within the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The created instance.
        /// </returns>
        protected override ConnectionSettingsSerializer CreateInstance(IContext context)
        {
            var supportedTypes =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => !type.IsAbstract)
                    .Where(type => typeof(ConnectionSettings).IsAssignableFrom(type))
                    .ToArray();

            return new ConnectionSettingsSerializer(supportedTypes);
        }
    }
}
