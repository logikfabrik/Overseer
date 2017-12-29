namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Repositories
    {
        [JsonProperty(PropertyName = "repositories")]
        public IEnumerable<Repository> Records { get; set; }
    }
}
