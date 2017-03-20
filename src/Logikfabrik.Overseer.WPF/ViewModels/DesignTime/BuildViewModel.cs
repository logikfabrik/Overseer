using System;
using System.Collections.Generic;

namespace Logikfabrik.Overseer.WPF.ViewModels.DesignTime
{
    public class BuildViewModel : IBuildViewModel
    {
        public string Id { get; } = "1234";

        public string Name { get; } = "1234";

        public string RequestedBy { get; } = "John Doe";

        public bool ShowRequestedBy { get; } = true;

        public string Branch { get; } = "master";

        public bool ShowBranch { get; } = true;

        public string Message { get; } = "Message goes here";

        public BuildStatus? Status { get; } = BuildStatus.Succeeded;

        public DateTime? StartTime { get; }

        public DateTime? EndTime { get; }

        public IEnumerable<IChangeViewModel> Changes { get; } = new ChangeViewModel [] { new ChangeViewModel() };
    }
}
