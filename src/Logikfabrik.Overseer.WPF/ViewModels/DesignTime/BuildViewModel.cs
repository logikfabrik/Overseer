// <copyright file="BuildViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.DesignTime
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="BuildViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class BuildViewModel : IBuildViewModel
    {
        /// <inheritdoc />
        public string Id { get; } = "1234";

        /// <inheritdoc />
        public string Name { get; } = "1234";

        /// <inheritdoc />
        public string VersionNumber { get; } = "100";

        /// <inheritdoc />
        public string RequestedBy { get; } = "John Doe";

        /// <inheritdoc />
        public string Branch { get; } = "master";

        /// <inheritdoc />
        public string Message { get; } = "Message goes here";

        /// <inheritdoc />
        public BuildStatus? Status { get; } = BuildStatus.Succeeded;

        /// <inheritdoc />
        public DateTime? StartTime { get; } = DateTime.UtcNow;

        /// <inheritdoc />
        public DateTime? EndTime { get; } = DateTime.UtcNow;

        /// <inheritdoc />
        public IEnumerable<IChangeViewModel> Changes { get; } = new[] { new ChangeViewModel(), new ChangeViewModel(), new ChangeViewModel() };

        /// <inheritdoc />
        public bool IsViewable { get; } = true;

        /// <inheritdoc />
        public bool TryUpdate(IProject project, IBuild build)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void View()
        {
            throw new NotImplementedException();
        }
    }
}
