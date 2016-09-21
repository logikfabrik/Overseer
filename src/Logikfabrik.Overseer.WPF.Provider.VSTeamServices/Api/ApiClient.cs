// <copyright file="ApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Api
{
    using System;
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
        /// <param name="baseUri">The base URI.</param>
        /// <param name="token">The token.</param>
        public ApiClient(string baseUri, string token)
        {
            Ensure.That(baseUri).IsNotNullOrWhiteSpace();
            Ensure.That(token).IsNotNullOrWhiteSpace();

            _baseUri = new Uri(baseUri);
            _token = token;
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>A task.</returns>
        public async Task<Projects> GetProjectsAsync()
        {
            using (var client = GetHttpClient())
            {
                using (var response = await client.GetAsync("_apis/projects?stateFilter=All&api-version=1.0").ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsAsync<Projects>().ConfigureAwait(false);
                }
            }
        }

        private HttpClient GetHttpClient()
        {
            var credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{string.Empty}:{_token}"));

            var client = new HttpClient { BaseAddress = _baseUri };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            return client;
        }
    }
}
