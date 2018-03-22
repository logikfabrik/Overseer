// <copyright file="BuildStatusMessageLocalizer.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Localization
{
    using System;
    using Humanizer;
    using Overseer.Extensions;
    using Properties;

    /// <summary>
    /// The <see cref="BuildStatusMessageLocalizer" /> class.
    /// </summary>
    public static class BuildStatusMessageLocalizer
    {
        public static string Localize(BuildStatus? status, TimeSpan? runTime, DateTime? endTime)
        {
            if (status.IsQueued())
            {
                return GetLocalizedBuildStatusMessageWhenQueued();
            }

            if (status.IsInProgress())
            {
                return GetLocalizedBuildStatusMessageWhenInProgress(runTime);
            }

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (status.IsFinished())
            {
                // ReSharper disable once PossibleInvalidOperationException
                return GetLocalizedBuildStatusMessageWhenFinished(status.Value, runTime, endTime);
            }

            return null;
        }

        private static string GetLocalizedBuildStatusMessageWhenQueued()
        {
            return Resources.BuildMessage_Queued;
        }

        private static string GetLocalizedBuildStatusMessageWhenInProgress(TimeSpan? runTime)
        {
            return !runTime.HasValue
                ? Resources.BuildMessage_InProgress
                : string.Format(Resources.BuildMessage_InProgressWithRunTime, runTime.Value.Humanize());
        }

        private static string GetLocalizedBuildStatusMessageWhenFinished(BuildStatus status, TimeSpan? runTime, DateTime? endTime)
        {
            string message;
            string messageWithRunTime;
            string messageWithEndTime;
            string messageWithRunTimeAndEndTime;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (status)
            {
                case BuildStatus.Failed:
                    message = Resources.BuildMessage_Failed;
                    messageWithRunTime = Resources.BuildMessage_FailedWithRunTime;
                    messageWithEndTime = Resources.BuildMessage_FailedWithEndTime;
                    messageWithRunTimeAndEndTime = Resources.BuildMessage_FailedWithRunTimeAndEndTime;
                    break;

                case BuildStatus.Succeeded:
                    message = Resources.BuildMessage_Succeeded;
                    messageWithRunTime = Resources.BuildMessage_SucceededWithRunTime;
                    messageWithEndTime = Resources.BuildMessage_SucceededWithEndTime;
                    messageWithRunTimeAndEndTime = Resources.BuildMessage_SucceededWithRunTimeAndEndTime;
                    break;

                case BuildStatus.Stopped:
                    message = Resources.BuildMessage_Stopped;
                    messageWithRunTime = Resources.BuildMessage_StoppedWithRunTime;
                    messageWithEndTime = Resources.BuildMessage_StoppedWithEndTime;
                    messageWithRunTimeAndEndTime = Resources.BuildMessage_StoppedWithRunTimeAndEndTime;
                    break;

                default:
                    throw new NotSupportedException();
            }

            if (!runTime.HasValue)
            {
                return !endTime.HasValue
                    ? message
                    : string.Format(messageWithEndTime, endTime.Value.Humanize());
            }

            return !endTime.HasValue
                ? string.Format(messageWithRunTime, runTime.Value.Humanize())
                : string.Format(messageWithRunTimeAndEndTime, runTime.Value.Humanize(), endTime.Value.Humanize());
        }
    }
}
