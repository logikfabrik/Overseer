// <copyright file="IViewProjectViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// The <see cref="IViewProjectViewModel" /> interface.
    /// </summary>
    public interface IViewProjectViewModel
    {
        /// <summary>
        /// Gets the settings identifier.
        /// </summary>
        /// <value>
        /// The settings identifier.
        /// </value>
        Guid SettingsId { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

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
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        bool IsBusy { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is viewable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is viewable; otherwise, <c>false</c>.
        /// </value>
        bool IsViewable { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is errored.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is errored; otherwise, <c>false</c>.
        /// </value>
        bool IsErrored { get; set; }

        /// <summary>
        /// Gets the favorite.
        /// </summary>
        /// <value>
        /// The favorite.
        /// </value>
        EditFavoriteViewModel Favorite { get; }

        /// <summary>
        /// Views this instance.
        /// </summary>
        void View();
    }
}