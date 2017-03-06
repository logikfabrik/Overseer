// <copyright file="TestOccurrences.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Api.Models
{
    /// <summary>
    /// The <see cref="TestOccurrences" /> class.
    /// </summary>
    public class TestOccurrences
    {
        /// <summary>
        /// Gets or sets the number of tests.
        /// </summary>
        /// <value>
        /// The number of tests.
        /// </value>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the number of passed tests.
        /// </summary>
        /// <value>
        /// The number of passed tests.
        /// </value>
        public int Passed { get; set; }

        /// <summary>
        /// Gets or sets the number of failed tests.
        /// </summary>
        /// <value>
        /// The number of failed tests.
        /// </value>
        public int Failed { get; set; }

        /// <summary>
        /// Gets or sets the number of muted tests.
        /// </summary>
        /// <value>
        /// The number of muted tests.
        /// </value>
        public int Muted { get; set; }
    }
}
