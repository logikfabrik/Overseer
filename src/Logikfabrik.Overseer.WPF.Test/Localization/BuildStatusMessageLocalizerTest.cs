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
            var buildStatusMessage = BuildStatusMessageLocalizer.Localize(status, runTime, endTime);

            buildStatusMessage.ShouldBe(expected);
        }

        private class CanGetBuildRunTimeMessageClassData : IEnumerable<object[]>
        {
            private readonly IEnumerable<object[]> _data = new[]
            {
                new object[] { null, null, null, null },
                new object[] { null, null, DateTime.UtcNow, null },
                new object[] { null, TimeSpan.FromHours(1), null, null },
                new object[] { null, TimeSpan.FromHours(1), DateTime.UtcNow, null },

                new object[] { BuildStatus.Failed, null, null, Resources.BuildMessage_Failed },
                new object[] { BuildStatus.Failed, null, DateTime.UtcNow, string.Format(Resources.BuildMessage_FailedWithEndTime, "now") },
                new object[] { BuildStatus.Failed, TimeSpan.FromHours(1), null, string.Format(Resources.BuildMessage_FailedWithRunTime, "1 hour") },
                new object[] { BuildStatus.Failed, TimeSpan.FromHours(1), DateTime.UtcNow, string.Format(Resources.BuildMessage_FailedWithRunTimeAndEndTime, "1 hour", "now") },

                new object[] { BuildStatus.Succeeded, null, null, Resources.BuildMessage_Succeeded },
                new object[] { BuildStatus.Succeeded, null, DateTime.UtcNow, string.Format(Resources.BuildMessage_SucceededWithEndTime, "now") },
                new object[] { BuildStatus.Succeeded, TimeSpan.FromHours(1), null, string.Format(Resources.BuildMessage_SucceededWithRunTime, "1 hour") },
                new object[] { BuildStatus.Succeeded, TimeSpan.FromHours(1), DateTime.UtcNow, string.Format(Resources.BuildMessage_SucceededWithRunTimeAndEndTime, "1 hour", "now") },

                new object[] { BuildStatus.InProgress, null, null, Resources.BuildMessage_InProgress},
                new object[] { BuildStatus.InProgress, null, DateTime.UtcNow, Resources.BuildMessage_InProgress },
                new object[] { BuildStatus.InProgress, TimeSpan.FromHours(1), null, string.Format(Resources.BuildMessage_InProgressWithRunTime, "1 hour") },
                new object[] { BuildStatus.InProgress, TimeSpan.FromHours(1), DateTime.UtcNow, string.Format(Resources.BuildMessage_InProgressWithRunTime, "1 hour") },

                new object[] { BuildStatus.Stopped, null, null, Resources.BuildMessage_Stopped },
                new object[] { BuildStatus.Stopped, null, DateTime.UtcNow, string.Format(Resources.BuildMessage_StoppedWithEndTime, "now") },
                new object[] { BuildStatus.Stopped, TimeSpan.FromHours(1), null, string.Format(Resources.BuildMessage_StoppedWithRunTime, "1 hour") },
                new object[] { BuildStatus.Stopped, TimeSpan.FromHours(1), DateTime.UtcNow, string.Format(Resources.BuildMessage_StoppedWithRunTimeAndEndTime, "1 hour", "now") },

                new object[] { BuildStatus.Queued, null, null, Resources.BuildMessage_Queued},
                new object[] { BuildStatus.Queued, null, DateTime.UtcNow, Resources.BuildMessage_Queued },
                new object[] { BuildStatus.Queued, TimeSpan.FromHours(1), null, Resources.BuildMessage_Queued },
                new object[] { BuildStatus.Queued, TimeSpan.FromHours(1), DateTime.UtcNow, Resources.BuildMessage_Queued }
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
