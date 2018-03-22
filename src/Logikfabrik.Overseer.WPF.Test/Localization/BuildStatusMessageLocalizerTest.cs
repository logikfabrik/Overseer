// <copyright file="BuildStatusMessageLocalizerTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Test.Localization
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Properties;
    using Shouldly;
    using WPF.Localization;
    using Xunit;

    public class BuildStatusMessageLocalizerTest
    {
        [Theory]
        [ClassData(typeof(CanGetBuildRunTimeMessageClassData))]
        public void CanLocalize(BuildStatus? status, TimeSpan? runTime, DateTime? endTime, string expected)
        {
            var statusMessage = BuildStatusMessageLocalizer.Localize(status, runTime, endTime);

            statusMessage.ShouldBe(expected);
        }

        private class CanGetBuildRunTimeMessageClassData : IEnumerable<object[]>
        {
            private readonly IEnumerable<object[]> _data = new[]
            {
                new object[] { null, null, null, null },
                new object[] { null, null, DateTime.UtcNow, null },
                new object[] { null, TimeSpan.FromHours(1), null, null },
                new object[] { null, TimeSpan.FromHours(1), DateTime.UtcNow, null },

                new object[] { BuildStatus.Failed, null, null, Resources.BuildStatusMessage_Failed },
                new object[] { BuildStatus.Failed, null, DateTime.UtcNow, string.Format(Resources.BuildStatusMessage_FailedWithEndTime, "now") },
                new object[] { BuildStatus.Failed, TimeSpan.FromHours(1), null, string.Format(Resources.BuildStatusMessage_FailedWithRunTime, "1 hour") },
                new object[] { BuildStatus.Failed, TimeSpan.FromHours(1), DateTime.UtcNow, string.Format(Resources.BuildStatusMessage_FailedWithRunTimeAndEndTime, "1 hour", "now") },

                new object[] { BuildStatus.Succeeded, null, null, Resources.BuildStatusMessage_Succeeded },
                new object[] { BuildStatus.Succeeded, null, DateTime.UtcNow, string.Format(Resources.BuildStatusMessage_SucceededWithEndTime, "now") },
                new object[] { BuildStatus.Succeeded, TimeSpan.FromHours(1), null, string.Format(Resources.BuildStatusMessage_SucceededWithRunTime, "1 hour") },
                new object[] { BuildStatus.Succeeded, TimeSpan.FromHours(1), DateTime.UtcNow, string.Format(Resources.BuildStatusMessage_SucceededWithRunTimeAndEndTime, "1 hour", "now") },

                new object[] { BuildStatus.InProgress, null, null, Resources.BuildStatusMessage_InProgress },
                new object[] { BuildStatus.InProgress, null, DateTime.UtcNow, Resources.BuildStatusMessage_InProgress },
                new object[] { BuildStatus.InProgress, TimeSpan.FromHours(1), null, string.Format(Resources.BuildStatusMessage_InProgressWithRunTime, "1 hour") },
                new object[] { BuildStatus.InProgress, TimeSpan.FromHours(1), DateTime.UtcNow, string.Format(Resources.BuildStatusMessage_InProgressWithRunTime, "1 hour") },

                new object[] { BuildStatus.Stopped, null, null, Resources.BuildStatusMessage_Stopped },
                new object[] { BuildStatus.Stopped, null, DateTime.UtcNow, string.Format(Resources.BuildStatusMessage_StoppedWithEndTime, "now") },
                new object[] { BuildStatus.Stopped, TimeSpan.FromHours(1), null, string.Format(Resources.BuildStatusMessage_StoppedWithRunTime, "1 hour") },
                new object[] { BuildStatus.Stopped, TimeSpan.FromHours(1), DateTime.UtcNow, string.Format(Resources.BuildStatusMessage_StoppedWithRunTimeAndEndTime, "1 hour", "now") },

                new object[] { BuildStatus.Queued, null, null, Resources.BuildStatusMessage_Queued },
                new object[] { BuildStatus.Queued, null, DateTime.UtcNow, Resources.BuildStatusMessage_Queued },
                new object[] { BuildStatus.Queued, TimeSpan.FromHours(1), null, Resources.BuildStatusMessage_Queued },
                new object[] { BuildStatus.Queued, TimeSpan.FromHours(1), DateTime.UtcNow, Resources.BuildStatusMessage_Queued }
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
