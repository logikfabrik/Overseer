// <copyright file="AppCatalog.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using EnsureThat;
    using Ninject.Modules;

    /// <summary>
    /// The <see cref="AppCatalog" /> class.
    /// </summary>
    public class AppCatalog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppCatalog" /> class.
        /// </summary>
        /// <param name="product">The product.</param>
        public AppCatalog(string product)
        {
            Ensure.That(product).IsNotNullOrWhiteSpace();

            Assemblies = GetAssemblies(product);
            Modules = GetModules(Assemblies);
        }

        /// <summary>
        /// Gets the assemblies.
        /// </summary>
        /// <value>
        /// The assemblies.
        /// </value>
        public IEnumerable<Assembly> Assemblies { get; }

        /// <summary>
        /// Gets the modules.
        /// </summary>
        /// <value>
        /// The modules.
        /// </value>
        public IEnumerable<Assembly> Modules { get; }

        /// <summary>
        /// Gets the product.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>The product.</returns>
        public static string GetProduct(Assembly assembly)
        {
            Ensure.That(assembly).IsNotNull();

            return assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
        }

        private static IEnumerable<Assembly> GetAssemblies(string product)
        {
            Func<Assembly, bool> predicate = assembly => GetProduct(assembly) == product;

            AssemblyLoader.Load(predicate);

            return AppDomain.CurrentDomain.GetAssemblies().Where(predicate);
        }

        private static IEnumerable<Assembly> GetModules(IEnumerable<Assembly> assemblies)
        {
            return assemblies.Where(assembly => assembly.GetExportedTypes().Any(type => !type.IsAbstract && typeof(INinjectModule).IsAssignableFrom(type)));
        }
    }
}
