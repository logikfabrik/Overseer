﻿// <copyright file="ApiClient.cs" company="Logikfabrik">
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
        /// <param name="url">The URL.</param>
        /// <param name="token">The token.</param>
        public ApiClient(string url, string token)
        {
            Ensure.That(url).IsNotNullOrWhiteSpace();
            Ensure.That(token).IsNotNullOrWhiteSpace();

            _baseUri = new Uri(url);
            _token = token;
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="skip">The skip count.</param>
        /// <param name="take">The take count.</param>
        /// <returns>A task.</returns>
        public async Task<Projects> GetProjectsAsync(int skip, int take)
        {
            using (var client = GetHttpClient())
            {
                using (var response = await client.GetAsync($"_apis/projects?api-version=2.0&$skip={skip}&$top={take}").ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsAsync<Projects>().ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Gets the builds.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="skip">The skip count.</param>
        /// <param name="take">The take count.</param>
        /// <returns>A task.</returns>
        public async Task<Builds> GetBuildsAsync(string projectId, int skip, int take)
        {
            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            using (var client = GetHttpClient())
            {
                using (var response = await client.GetAsync($"{projectId}/_apis/build/builds?api-version=2.0&$skip={skip}&$top={take}").ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsAsync<Builds>().ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Gets the changes.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="buildId">The build identifier.</param>
        /// <returns>A task.</returns>
        public async Task<Changes> GetChangesAsync(string projectId, string buildId)
        {
            Ensure.That(projectId).IsNotNullOrWhiteSpace();
            Ensure.That(buildId).IsNotNullOrWhiteSpace();

            using (var client = GetHttpClient())
            {
                using (var response = await client.GetAsync($"{projectId}/_apis/build/builds/{buildId}/changes?api-version=2.0").ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsAsync<Changes>().ConfigureAwait(false);
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
