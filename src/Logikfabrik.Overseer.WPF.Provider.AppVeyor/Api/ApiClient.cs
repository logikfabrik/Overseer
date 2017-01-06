// <copyright file="ApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.Api
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using EnsureThat;
    using Models;

    /// <summary>
    /// The <see cref="ApiClient" /> class.
    /// </summary>
    public class ApiClient : IDisposable
    {
        private readonly Lazy<HttpClient> _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="token">The token.</param>
        public ApiClient(string token)
        {
            Ensure.That(token).IsNotNullOrWhiteSpace();

            _httpClient = new Lazy<HttpClient>(() => GetHttpClient(new Uri("https://ci.appveyor.com/"), token));
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        public async Task<IEnumerable<Project>> GetProjectsAsync(CancellationToken cancellationToken)
        {
            using (var response = await _httpClient.Value.GetAsync("api/projects", cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<IEnumerable<Project>>(cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets the project history.
        /// </summary>
        /// <param name="accountName">The account name.</param>
        /// <param name="projectSlug">The project slug.</param>
        /// <param name="recordsNumber">Number of records.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        public async Task<ProjectHistory> GetProjectHistoryAsync(string accountName, string projectSlug, int recordsNumber, CancellationToken cancellationToken)
        {
            Ensure.That(accountName).IsNotNullOrWhiteSpace();
            Ensure.That(projectSlug).IsNotNullOrWhiteSpace();

            using (var response = await _httpClient.Value.GetAsync($"api/projects/{accountName}/{projectSlug}/history?recordsNumber={recordsNumber}", cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<ProjectHistory>(cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // ReSharper disable once InvertIf
            if (disposing)
            {
                if (!_httpClient.IsValueCreated)
                {
                    return;
                }

                _httpClient.Value.CancelPendingRequests();
                _httpClient.Value.Dispose();
            }
        }

        private static HttpClient GetHttpClient(Uri baseUri, string token)
        {
            var client = new HttpClient { BaseAddress = baseUri };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }
    }
}