// <copyright file="ApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.Api
{
    using System;
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
        private readonly Uri _baseUri;
        private readonly string _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="token">The token.</param>
        public ApiClient(string token)
        {
            Ensure.That(token).IsNotNullOrWhiteSpace();

            _baseUri = new Uri("https://ci.appveyor.com/");
            _token = token;
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>A task.</returns>
        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            using (var client = GetHttpClient())
            {
                using (var response = await client.GetAsync("api/projects").ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsAsync<IEnumerable<Project>>().ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Gets the project history.
        /// </summary>
        /// <param name="accountName">The account name.</param>
        /// <param name="projectSlug">The project slug.</param>
        /// <param name="recordsNumber">Number of records.</param>
        /// <returns>A task.</returns>
        public async Task<ProjectHistory> GetProjectHistoryAsync(string accountName, string projectSlug, int recordsNumber)
        {
            Ensure.That(accountName).IsNotNullOrWhiteSpace();
            Ensure.That(projectSlug).IsNotNullOrWhiteSpace();

            using (var client = GetHttpClient())
            {
                using (var response = await client.GetAsync($"api/projects/{accountName}/{projectSlug}/history?recordsNumber={recordsNumber}").ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsAsync<ProjectHistory>().ConfigureAwait(false);
                }
            }
        }

        private HttpClient GetHttpClient()
        {
            var client = new HttpClient { BaseAddress = _baseUri };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            return client;
        }
    }
}