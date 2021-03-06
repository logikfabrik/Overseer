﻿// <copyright file="Changes.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Api.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="Changes" /> class.
    /// </summary>
    public class Changes
    {
        /// <summary>
        /// Gets or sets the changes count.
        /// </summary>
        /// <value>
        /// The changes count.
        /// </value>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public IEnumerable<Change> Value { get; set; }
    }
}
