// <copyright file="AssemblyLoader.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// The <see cref="AssemblyLoader" /> class.
    /// </summary>
    public class AssemblyLoader
    {
        /// <summary>
        /// Loads assemblies into the current <see cref="AppDomain" />.
        /// </summary>
        public void Load()
        {
            var paths = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.TopDirectoryOnly);
            
            foreach (var path in paths)
            {
                AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path));
            }
        }
    }
}
