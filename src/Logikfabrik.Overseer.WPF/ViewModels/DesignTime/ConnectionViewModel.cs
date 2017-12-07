// <copyright file="ConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.DesignTime
{
    using System;
    using System.ComponentModel;
    using System.Windows.Data;

    /// <summary>
    /// The <see cref="ConnectionViewModel" /> class.
    /// </summary>
    public class ConnectionViewModel : IConnectionViewModel
    {
        /// <inheritdoc/>
        public Guid SettingsId { get; } = Guid.NewGuid();

        /// <inheritdoc/>
        public string SettingsName { get; set; } = "My Connection";

        /// <inheritdoc/>
        public bool IsBusy { get; } = false;

        /// <inheritdoc/>
        public bool IsViewable { get; } = true;

        /// <inheritdoc/>
        public bool IsEditable { get; } = true;

        /// <inheritdoc/>
        public bool IsErrored { get; } = false;

        /// <inheritdoc/>
        public ICollectionView FilteredProjects { get; } = new CollectionView(new[] { new ProjectViewModel(), new ProjectViewModel(), new ProjectViewModel() });

        /// <inheritdoc/>
        public string Filter { get; set; }

        /// <inheritdoc/>
        public bool HasProjects { get; } = true;

        /// <inheritdoc/>
        public bool HasNoProjects { get; } = false;

        /// <inheritdoc/>
        public string DisplayName { get; set; }

        /// <inheritdoc/>
        public object Parent { get; set; }

        /// <inheritdoc/>
        public bool KeepAlive { get; set; }

        /// <inheritdoc/>
        public void Edit()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Remove()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void View()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void TryClose(bool? dialogResult = null)
        {
            throw new NotImplementedException();
        }
    }
}
