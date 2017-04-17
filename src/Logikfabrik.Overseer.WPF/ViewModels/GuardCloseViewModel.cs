// <copyright file="GuardCloseViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Caliburn.Micro;

    public abstract class GuardCloseViewModel : ViewModel, IGuardClose
    {
        public void TryClose(bool? dialogResult = null)
        {
            throw new NotImplementedException();
        }

        public void CanClose(Action<bool> callback)
        {
            throw new NotImplementedException();
        }
    }
}
