// <copyright file="AboutViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using System.Reflection;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="AboutViewModel" /> class.
    /// </summary>
    public class AboutViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutViewModel" /> class.
        /// </summary>
        public AboutViewModel()
        {
            Version = GetType().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            DisplayName = Properties.Resources.About_View;
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; }
    }
}
