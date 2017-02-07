// <copyright file="INotifyTask.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System.Threading.Tasks;
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="INotifyTask" /> interface.
    /// </summary>
    public interface INotifyTask : INotifyPropertyChangedEx
    {
        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        TaskStatus? Status { get; }
    }
}
