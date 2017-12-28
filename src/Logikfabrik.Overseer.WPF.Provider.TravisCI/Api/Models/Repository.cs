namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api.Models
{
    public class Repository
    {
        public string id { get; set; }
        public string slug { get; set; }
        public string description { get; set; }
        public string last_build_id { get; set; }
        public string last_build_number { get; set; }
        public string last_build_state { get; set; }
        public string last_build_duration { get; set; }
        public string last_build_started_at { get; set; }
        public string last_build_finished_at { get; set; }
        public bool active { get; set; }
    }
}
