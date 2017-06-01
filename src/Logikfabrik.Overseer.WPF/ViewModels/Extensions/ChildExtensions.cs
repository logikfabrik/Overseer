// <copyright file="ChildExtensions.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Extensions
{
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="ChildExtensions" /> class.
    /// </summary>
    public static class ChildExtensions
    {
        /// <summary>
        /// Gets the conductor of the specified child.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="child">The child.</param>
        /// <returns>The conductor.</returns>
        public static Conductor<T>.Collection.OneActive GetConductor<T>(this IChild child)
            where T : class
        {
            return child.Parent as Conductor<T>.Collection.OneActive;
        }
    }
}
