// <copyright file="ViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="ViewModel" /> class. Base class for view models intended to be accessed using a <see cref="Conductor{IViewModel}.Collection.OneActive" /> implementation.
    /// </summary>
    public abstract class ViewModel : ViewAware, IViewModel
    {
        private object _parent;
        private string _displayName;

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public virtual object Parent
        {
            get
            {
                return _parent;
            }

            set
            {
                _parent = value;
                NotifyOfPropertyChange(() => Parent);
            }
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName
        {
            get
            {
                return _displayName;
            }

            set
            {
                _displayName = value;
                NotifyOfPropertyChange(() => DisplayName);
            }
        }

        public void TryClose(bool? dialogResult = null)
        {
            PlatformProvider.Current.GetViewCloseAction(this, Views.Values, dialogResult).OnUIThread();
        }

        protected IEnumerable<T> GetOpenChildren<T>()
            where T : IViewModel
        {
            var conductor = GetConductor();

            return conductor?.GetChildren().OfType<T>() ?? new T[] { };
        }

        protected void CloseChild(IViewModel child)
        {
            var conductor = GetConductor();

            conductor?.CloseItem(child);
        }

        private Conductor<IViewModel>.Collection.OneActive GetConductor()
        {
            return Parent as Conductor<IViewModel>.Collection.OneActive;
        }
    }
}
