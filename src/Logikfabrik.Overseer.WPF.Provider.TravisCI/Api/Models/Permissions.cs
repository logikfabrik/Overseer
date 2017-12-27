// <copyright file="Permissions.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Permissions
    {
        [JsonProperty(PropertyName = "permissions")]
        public IEnumerable<string> Repositories { get; set; }
    }
}
