// <copyright file="IViewProjectViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.ComponentModel;

    /// <summary>
    /// The <see cref="IViewProjectViewModel" /> interface.
    /// </summary>
    public interface IViewProjectViewModel
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets the ordered builds.
        /// </summary>
        /// <value>
        /// The ordered builds.
        /// </value>
        ICollectionView OrderedBuilds { get; }

        /// <summary>
        /// Gets the latest in progress or finished build.
        /// </summary>
        /// <value>
        /// The latest in progress or finished build.
        /// </value>
        IViewBuildViewModel LatestBuild { get; }

        /// <summary>
        /// Gets the number of queued builds.
        /// </summary>
        /// <value>
        /// The number of queued builds.
        /// </value>
        int QueuedBuilds { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has builds.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has builds; otherwise, <c>false</c>.
        /// </value>
        bool HasBuilds { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has no builds.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has no builds; otherwise, <c>false</c>.
        /// </value>
        bool HasNoBuilds { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has a latest build.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has a latest build; otherwise, <c>false</c>.
        /// </value>
        bool HasLatestBuild { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has queued builds.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has queued builds; otherwise, <c>false</c>.
        /// </value>
        bool HasQueuedBuilds { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        bool IsBusy { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is viewable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is viewable; otherwise, <c>false</c>.
        /// </value>
        bool IsViewable { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is errored.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is errored; otherwise, <c>false</c>.
        /// </value>
        bool IsErrored { get; }

        EditFavoriteViewModel Favorite { get; }

        /// <summary>
        /// Views this instance.
        /// </summary>
        void View();

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="name">The name.</param>s
        void Update(string name);
    }
}