// <copyright file="Module.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices
{
    using Ninject.Modules;
    using ViewModels;

    /// <summary>
    /// The <see cref="Module" /> class.
    /// </summary>
    /// <seealso cref="NinjectModule" />
    public class Module : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<WPF.ViewModels.BuildProviderViewModel>().To<BuildProviderViewModel>();
        }
    }
}
