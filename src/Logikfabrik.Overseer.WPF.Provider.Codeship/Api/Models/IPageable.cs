// <copyright file="IPageable.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.Codeship.Api.Models
{
    /// <summary>
    /// The <see cref="IPageable" /> interface.
    /// </summary>
    public interface IPageable
    {
        /// <summary>
        /// Gets the total items.
        /// </summary>
        /// <value>
        /// The total items.
        /// </value>
        int Total { get; }

        /// <summary>
        /// Gets the items per page.
        /// </summary>
        /// <value>
        /// The items per page.
        /// </value>
        int PerPage { get; }

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        int Page { get; }
    }
}
