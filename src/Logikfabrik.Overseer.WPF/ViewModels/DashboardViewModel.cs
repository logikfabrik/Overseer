// <copyright file="DashboardViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="DashboardViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class DashboardViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public DashboardViewModel(IPlatformProvider platformProvider)
            : base(platformProvider)
        {
            DisplayName = Properties.Resources.Dashboard_View;
        }
    }
}
