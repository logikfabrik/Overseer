// <copyright file="ConnectionSettingsSerializerProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Providers.Settings
{
    using System;
    using System.Linq;
    using EnsureThat;
    using Ninject.Activation;
    using Overseer.Settings;

    /// <summary>
    /// The <see cref="ConnectionSettingsSerializerProvider" /> class.
    /// </summary>
    public class ConnectionSettingsSerializerProvider : Provider<IConnectionSettingsSerializer>
    {
        private readonly AppDomain _appDomain;

        public ConnectionSettingsSerializerProvider(AppDomain appDomain)
        {
            Ensure.That(appDomain).IsNotNull();

            _appDomain = appDomain;
        }

        /// <summary>
        /// Creates an instance within the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The created instance.
        /// </returns>
        protected override IConnectionSettingsSerializer CreateInstance(IContext context)
        {
            var supportedTypes =
                _appDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => !type.IsAbstract)
                    .Where(type => typeof(ConnectionSettings).IsAssignableFrom(type))
                    .ToArray();

            return new ConnectionSettingsSerializer(supportedTypes);
        }
    }
}
