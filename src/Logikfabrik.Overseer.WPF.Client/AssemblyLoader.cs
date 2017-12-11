// <copyright file="AssemblyLoader.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using EnsureThat;

    /// <summary>
    /// The <see cref="AssemblyLoader" /> class.
    /// </summary>
    public static class AssemblyLoader
    {
        /// <summary>
        /// Loads assemblies from the base directory into the current application domain.
        /// </summary>
        /// <param name="currentAppDomain">The current application domain.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Loaded assemblies.</returns>
        public static IEnumerable<AssemblyName> Load(AppDomain currentAppDomain, Func<Assembly, bool> predicate)
        {
            Ensure.That(currentAppDomain).IsNotNull();
            Ensure.That(predicate).IsNotNull();

            var appDomain = AppDomain.CreateDomain("temp");

            Load(currentAppDomain, appDomain);

            var assemblies = appDomain.GetAssemblies().Where(predicate).Select(assembly => assembly.GetName());

            AppDomain.Unload(appDomain);

            return assemblies;
        }

        private static void Load(AppDomain currentAppDomain, AppDomain appDomain)
        {
            Ensure.That(appDomain).IsNotNull();

            var paths = Directory.EnumerateFiles(currentAppDomain.BaseDirectory, "*.dll", SearchOption.TopDirectoryOnly);

            foreach (var path in paths)
            {
                appDomain.Load(AssemblyName.GetAssemblyName(path));
            }
        }
    }
}