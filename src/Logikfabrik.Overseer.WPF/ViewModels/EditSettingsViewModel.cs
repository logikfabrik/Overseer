// <copyright file="EditSettingsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    /// <summary>
    /// The <see cref="EditSettingsViewModel" /> class. View model for editing application wide settings.
    /// </summary>
    public class EditSettingsViewModel : ViewModel
    {
        /// <summary>
        /// Gets the view name.
        /// </summary>
        /// <value>
        /// The view name.
        /// </value>
        public override string ViewName { get; } = "Settings";
    }
}
