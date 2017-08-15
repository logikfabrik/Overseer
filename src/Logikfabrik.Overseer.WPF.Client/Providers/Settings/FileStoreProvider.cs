// <copyright file="FileStoreProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Providers.Settings
{
    using System;
    using System.IO;
    using Ninject.Activation;
    using Overseer.Settings;

    /// <summary>
    /// The <see cref="FileStoreProvider" /> class.
    /// </summary>
    public class FileStoreProvider : Provider<IFileStore>
    {
        /// <summary>
        /// Creates an instance within the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The created instance.
        /// </returns>
        protected override IFileStore CreateInstance(IContext context)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Overseer", "Providers.xml");

            return new FileStore(path);
        }
    }
}
