// <copyright file="Job.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.Api.Models
{
    using System;

    /// <summary>
    /// The <see cref="Job" /> class.
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Gets or sets the job identifier.
        /// </summary>
        /// <value>
        /// The job identifier.
        /// </value>
        public string JobId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether failures are allowed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if failures are allowed; otherwise, <c>false</c>.
        /// </value>
        public bool AllowFailure { get; set; }

        /// <summary>
        /// Gets or sets the messages count.
        /// </summary>
        /// <value>
        /// The messages count.
        /// </value>
        public int MessagesCount { get; set; }

        /// <summary>
        /// Gets or sets the compilation messages count.
        /// </summary>
        /// <value>
        /// The compilation messages count.
        /// </value>
        public int CompilationMessagesCount { get; set; }

        /// <summary>
        /// Gets or sets the compilation errors count.
        /// </summary>
        /// <value>
        /// The compilation errors count.
        /// </value>
        public int CompilationErrorsCount { get; set; }

        /// <summary>
        /// Gets or sets the compilation warnings count.
        /// </summary>
        /// <value>
        /// The compilation warnings count.
        /// </value>
        public int CompilationWarningsCount { get; set; }

        /// <summary>
        /// Gets or sets the tests count.
        /// </summary>
        /// <value>
        /// The tests count.
        /// </value>
        public int TestsCount { get; set; }

        /// <summary>
        /// Gets or sets the passed tests count.
        /// </summary>
        /// <value>
        /// The passed tests count.
        /// </value>
        public int PassedTestsCount { get; set; }

        /// <summary>
        /// Gets or sets the failed tests count.
        /// </summary>
        /// <value>
        /// The failed tests count.
        /// </value>
        public int FailedTestsCount { get; set; }

        /// <summary>
        /// Gets or sets the artifacts count.
        /// </summary>
        /// <value>
        /// The artifacts count.
        /// </value>
        public int ArtifactsCount { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the started date.
        /// </summary>
        /// <value>
        /// The started date.
        /// </value>
        public DateTime? Started { get; set; }

        /// <summary>
        /// Gets or sets the finished date.
        /// </summary>
        /// <value>
        /// The finished date.
        /// </value>
        public DateTime? Finished { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime? Created { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public DateTime? Updated { get; set; }
    }
}
