// <copyright file="IProjectDigestViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="IProjectDigestViewModelFactory" /> interface.
    /// </summary>
    public interface IProjectDigestViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="builds">The builds.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        ProjectDigestViewModel CreateProjectDigestViewModel(IEnumerable<IBuild> builds);
    }
}
