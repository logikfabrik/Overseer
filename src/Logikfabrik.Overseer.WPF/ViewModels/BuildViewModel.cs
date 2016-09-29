// <copyright file="BuildViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Caliburn.Micro;
    using EnsureThat;
    using Humanizer;

    /// <summary>
    /// The <see cref="BuildViewModel" /> class.
    /// </summary>
    public class BuildViewModel : PropertyChangedBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildViewModel" /> class.
        /// </summary>
        /// <param name="build">The build.</param>
        public BuildViewModel(IBuild build)
        {
            Ensure.That(build).IsNotNull();

            Branch = build.Branch;

            VersionOrNumber = !string.IsNullOrWhiteSpace(build.Version) ? build.Version : build.Number;

            Description = GetDescription(build);

            ChangeViewModels = build.LastChanges.Select(lastChange => new ChangeViewModel(lastChange));
        }

        /// <summary>
        /// Gets the change view models.
        /// </summary>
        /// <value>
        /// The change view models.
        /// </value>
        public IEnumerable<ChangeViewModel> ChangeViewModels { get; }

        /// <summary>
        /// Gets the branch.
        /// </summary>
        /// <value>
        /// The branch.
        /// </value>
        public string Branch { get; }

        /// <summary>
        /// Gets the version or number.
        /// </summary>
        /// <value>
        /// The version or number.
        /// </value>
        public string VersionOrNumber { get; }

        /// <summary>
        /// Gets when built.
        /// </summary>
        /// <value>
        /// When built.
        /// </value>
        public string Description { get; }

        private static string GetDescription(IBuild build)
        {
            var builder = new StringBuilder();

            if (build.Started.HasValue && build.Finished.HasValue)
            {
                builder.Append($"Built {build.Started.Humanize()}");
                builder.Append($" in {(build.Finished.Value - build.Started.Value).Humanize()}");

                if (!string.IsNullOrWhiteSpace(build.RequestedBy))
                {
                    builder.Append($" for {build.RequestedBy}");
                }
            }
            else if (build.Started.HasValue)
            {
                builder.Append($"Building since {(DateTime.UtcNow - build.Started.Value).Humanize()} ago");

                if (!string.IsNullOrWhiteSpace(build.RequestedBy))
                {
                    builder.Append($" for {build.RequestedBy}");
                }
            }
            else
            {
                // TODO: Build has no started or finished time. What to report?
            }

            return builder.ToString();
        }
    }
}
