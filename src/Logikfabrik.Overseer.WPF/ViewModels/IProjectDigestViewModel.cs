// <copyright file="IProjectDigestViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    /// <summary>
    /// The <see cref="IProjectDigestViewModel" /> interface.
    /// </summary>
    public interface IProjectDigestViewModel
    {
        /// <summary>
        /// Gets the success rate.
        /// </summary>
        /// <value>
        /// The success rate.
        /// </value>
        double SuccessRate { get; }
    }
}