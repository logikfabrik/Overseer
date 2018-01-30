// <copyright file="ViewAboutViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Reflection;
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="ViewAboutViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ViewAboutViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewAboutViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public ViewAboutViewModel(IPlatformProvider platformProvider)
            : base(platformProvider)
        {
            Version = GetType().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            DisplayName = Properties.Resources.ViewAbout_View;
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
