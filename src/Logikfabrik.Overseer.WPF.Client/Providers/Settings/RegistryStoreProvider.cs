// <copyright file="RegistryStoreProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Providers.Settings
{
    using Ninject.Activation;
    using Overseer.Settings;

    /// <summary>
    /// The <see cref="RegistryStoreProvider" /> class.
    /// </summary>
    public class RegistryStoreProvider : Provider<RegistryStore>
    {
        /// <summary>
        /// Creates an instance within the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The created instance.
        /// </returns>
        protected override RegistryStore CreateInstance(IContext context)
        {
            return new RegistryStore("SOFTWARE\\Overseer");
        }
    }
}
