// <copyright file="IBuildViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="IBuildViewModelFactory" /> interface.
    /// </summary>
    public interface IBuildViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="projectName">The project name.</param>
        /// <param name="build">The build.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        BuildViewModel CreateBuildViewModel(string projectName, string id, string branch, string versionNumber, string requestedBy, IEnumerable<IChange> changes, BuildStatus? status, DateTime? startTime, DateTime? endTime, TimeSpan? runTime);
    }
}
