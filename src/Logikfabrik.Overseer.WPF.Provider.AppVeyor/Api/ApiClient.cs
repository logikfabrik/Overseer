// <copyright file="ApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.Api
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using EnsureThat;
    using Models;

    /// <summary>
    /// The <see cref="ApiClient" /> class.
    /// </summary>
    public class ApiClient
    {
        private const string BaseUri = "https://ci.appveyor.com/";
        private readonly string _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="token">The token.</param>
        public ApiClient(string token)
        {
            Ensure.That(token).IsNotNullOrWhiteSpace();

            _token = token;
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>The projects.</returns>
        public async Task<IEnumerable<Project>> GetProjects()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                using (var response = await client.GetAsync($"{BaseUri}api/projects"))
                {
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsAsync<IEnumerable<Project>>();
                }
            }
        }

        /// <summary>
        /// Gets the project history.
        /// </summary>
        /// <param name="accountName">The account name.</param>
        /// <param name="projectSlug">The project slug.</param>
        /// <returns>The project history.</returns>
        public async Task<ProjectHistory> GetProjectHistory(string accountName, string projectSlug)
        {
            Ensure.That(accountName).IsNotNullOrWhiteSpace();
            Ensure.That(projectSlug).IsNotNullOrWhiteSpace();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                using (var response = await client.GetAsync($"{BaseUri}api/projects/{accountName}/{projectSlug}/history?recordsNumber=10"))
                {
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsAsync<ProjectHistory>();
                }
            }
        }
    }
}