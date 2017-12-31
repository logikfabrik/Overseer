// <copyright file="NotifyTask.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using System.Threading.Tasks;
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="NotifyTask" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class NotifyTask : PropertyChangedBase, INotifyTask
    {
        private readonly Task _task;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyTask" /> class.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public NotifyTask()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyTask" /> class.
        /// </summary>
        /// <param name="task">The task.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public NotifyTask(Task task)
        {
            Ensure.That(task).IsNotNull();

            _task = task;

            if (_task.IsCompleted)
            {
                NotifyOfPropertyChange(() => Status);
                NotifyOfPropertyChange(() => Exception);

                return;
            }

            _task.ContinueWith(t =>
            {
                NotifyOfPropertyChange(() => Status);
                NotifyOfPropertyChange(() => Exception);
            });
        }

        /// <inheritdoc />
        public TaskStatus? Status => _task?.Status;

        /// <inheritdoc />
        public Exception Exception => _task?.Exception;
    }
}