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
    public abstract class ViewModel : ViewAware,  IViewModel
    {
        private object _parent;
        private string _displayName;

        public virtual object Parent
        {
            get
            {
                return this._parent;
            }
            set
            {
                this._parent = value;
                this.NotifyOfPropertyChange(() => Parent);
            }
        }

        /// <summary>Gets or Sets the Display Name</summary>
        public virtual string DisplayName
        {
            get
            {
                return this._displayName;
            }
            set
            {
                this._displayName = value;
                this.NotifyOfPropertyChange(() => DisplayName);
            }
        }

        private Conductor<IViewModel>.Collection.OneActive GetConductor()
        {
            return Parent as Conductor<IViewModel>.Collection.OneActive;
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

        public void TryClose(bool? dialogResult = null)
        {
            PlatformProvider.Current.GetViewCloseAction((object) this, this.Views.Values, dialogResult).OnUIThread();
        }
    }
}
