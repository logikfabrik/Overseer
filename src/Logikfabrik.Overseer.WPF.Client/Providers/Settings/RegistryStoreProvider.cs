// <copyright file="RegistryStoreProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Providers.Settings
{
    using IO.Registry;
    using Ninject.Activation;

    /// <summary>
    /// The <see cref="RegistryStoreProvider" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class RegistryStoreProvider : Provider<RegistryStore>
    {
        /// <inheritdoc />
        protected override RegistryStore CreateInstance(IContext context)
        {
            return new RegistryStore("SOFTWARE\\Overseer");
        }
    }
}
