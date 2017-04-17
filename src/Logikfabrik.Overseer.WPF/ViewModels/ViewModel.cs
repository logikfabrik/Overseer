// <copyright file="ViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="ViewModel" /> class. Base class for view models intended to be accessed using a <see cref="Conductor{IViewModel}" /> implementation.
    /// </summary>
    public abstract class ViewModel : Screen, IViewModel
    {
    }
}
