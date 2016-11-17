// <copyright file="ModuleHelper.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using System.Reflection;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ModuleHelper" /> class.
    /// </summary>
    public static class ModuleHelper
    {
        /// <summary>
        /// Gets the module name using the calling assembly.
        /// </summary>
        /// <returns>The module name.</returns>
        public static string GetModuleName()
        {
            var assembly = Assembly.GetCallingAssembly();

            return GetModuleName(assembly);
        }

        /// <summary>
        /// Gets the module name using the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The module name.</returns>
        public static string GetModuleName(object obj)
        {
            Ensure.That(obj).IsNotNull();

            var type = obj.GetType();

            return GetModuleName(type);
        }

        /// <summary>
        /// Gets the module name using the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The module name.</returns>
        public static string GetModuleName(Type type)
        {
            Ensure.That(type).IsNotNull();

            var assembly = type.Assembly;

            return GetModuleName(assembly);
        }

        /// <summary>
        /// Gets the module name using the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>The module name.</returns>
        public static string GetModuleName(Assembly assembly)
        {
            Ensure.That(assembly).IsNotNull();

            return assembly.FullName;
        }
    }
}
