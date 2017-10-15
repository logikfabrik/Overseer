// <copyright file="DashboardViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    /// <summary>
    /// The <see cref="DashboardViewModel" /> class.
    /// </summary>
    public class DashboardViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardViewModel" /> class.
        /// </summary>
        public DashboardViewModel()
        {
            DisplayName = Properties.Resources.Dashboard_View;
        }
    }
}
