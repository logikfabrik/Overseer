// <copyright file="ProjectDigestViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.DesignTime
{
    /// <summary>
    /// The <see cref="ProjectDigestViewModel" /> class.
    /// </summary>
    public class ProjectDigestViewModel : IProjectDigestViewModel
    {
        /// <summary>
        /// Gets the success rate.
        /// </summary>
        /// <value>
        /// The success rate.
        /// </value>
        public double SuccessRate { get; } = .5;
    }
}