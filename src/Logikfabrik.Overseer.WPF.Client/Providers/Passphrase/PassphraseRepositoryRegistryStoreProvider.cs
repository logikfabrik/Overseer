// <copyright file="PassphraseRepositoryRegistryStoreProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Providers.Passphrase
{
    using Ninject.Activation;
    using Overseer.IO.Registry;

    /// <summary>
    /// The <see cref="PassphraseRepositoryRegistryStoreProvider" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class PassphraseRepositoryRegistryStoreProvider : Provider<RegistryStore>
    {
        /// <inheritdoc />
        protected override RegistryStore CreateInstance(IContext context)
        {
            return new RegistryStore("SOFTWARE\\Overseer");
        }
    }
}
