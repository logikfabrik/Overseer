// <copyright file="ApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.Api
{
    /// <summary>
    /// The <see cref="ApiClient" /> class.
    /// </summary>
    public class ApiClient
    {
        //private readonly string _token;

        //public BuildProvider(Uri baseUri, string token)
        //{
        //    if (string.IsNullOrWhiteSpace(token))
        //    {
        //        throw new ArgumentException("Token cannot be null or white space.", nameof(token));
        //    }

        //    _token = token;
        //}

        //public async Task<IEnumerable<IProject>> GetProjects()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        //        // get the list of roles
        //        using (var response = await client.GetAsync("https://ci.appveyor.com/api/projects"))
        //        {
        //            response.EnsureSuccessStatusCode();

        //            var projects = await response.Content.ReadAsAsync<Project[]>();

        //            return projects;
        //        }
        //    }
        //}
    }
}
