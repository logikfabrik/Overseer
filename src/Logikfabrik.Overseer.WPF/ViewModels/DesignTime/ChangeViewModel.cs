// <copyright file="ChangeViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.DesignTime
{
    using System;

    /// <summary>
    /// The <see cref="ChangeViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ChangeViewModel : IChangeViewModel
    {
        /// <inheritdoc />
        public string Id { get; } = "2fd4e1c67a2d28fced849ee1bb76e7391b93eb12";

        /// <inheritdoc />
        public string IdOrShortId { get; } = "2fd4e1c6";

        /// <inheritdoc />
        public DateTime? Changed { get; } = DateTime.UtcNow;

        /// <inheritdoc />
        public string ChangedBy { get; } = "John Doe";

        /// <inheritdoc />
        public string Comment { get; } = "Minor change to the build definition";

        /// <inheritdoc />
        public bool ShowComment { get; } = true;
    }
}
