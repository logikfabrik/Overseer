// <copyright file="IBuildNotificationViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    /// <summary>
    /// The <see cref="IBuildNotificationViewModelFactory" /> interface.
    /// </summary>
    public interface IBuildNotificationViewModelFactory
    {
        BuildNotificationViewModel Create(IProject project, IBuild build);
    }
}
