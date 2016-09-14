namespace Logikfabrik.Overseer
{
    using System;

    public class StubBuild : IBuild
    {
        public string BuildNumber { get; set; }
        public string Branch { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Finished { get; set; }
        public BuildStatus? Status { get; set; }
    }
}
