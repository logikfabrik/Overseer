// <copyright file="ProjectViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.DesignTime
{
    using System;
    using System.ComponentModel;
    using System.Windows.Data;

    /// <summary>
    /// The <see cref="ProjectViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ProjectViewModel : IProjectViewModel
    {
        /// <inheritdoc />
        public string Id { get; } = "1234";

        /// <inheritdoc />
        public string Name { get; } = "Overseer";

        /// <inheritdoc />
        public ICollectionView OrderedBuilds { get; } = new CollectionView(new[] { new BuildViewModel(), new BuildViewModel(), new BuildViewModel() });

        /// <inheritdoc />
        public IBuildViewModel LatestBuild => new BuildViewModel();

        /// <inheritdoc />
        public int QueuedBuilds => 3;

        /// <inheritdoc />
        public bool HasBuilds { get; } = true;

        /// <inheritdoc />
        public bool HasNoBuilds { get; } = false;

        /// <inheritdoc />
        public bool HasLatestBuild { get; } = true;

        /// <inheritdoc />
        public bool HasQueuedBuilds => true;

        /// <inheritdoc />
        public bool IsBusy { get; } = false;

        /// <inheritdoc />
        public bool IsViewable { get; } = true;

        /// <inheritdoc />
        public bool IsErrored { get; } = false;

        /// <inheritdoc />
        public void View()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool TryUpdate(string name)
        {
            throw new NotImplementedException();
        }
    }
}