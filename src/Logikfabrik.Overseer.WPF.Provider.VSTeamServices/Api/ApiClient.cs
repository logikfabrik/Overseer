// <copyright file="ApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Api
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
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
        /// <param name="url">The URL.</param>
        /// <param name="token">The token.</param>
        public ApiClient(string url, string token)
        {
            Ensure.That(url).IsNotNullOrWhiteSpace();
            Ensure.That(token).IsNotNullOrWhiteSpace();

            _httpClient = new Lazy<HttpClient>(() => GetHttpClient(new Uri(url), token));
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="skip">The skip count.</param>
        /// <param name="take">The take count.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        public async Task<Projects> GetProjectsAsync(int skip, int? take, CancellationToken cancellationToken)
        {
            var builder = new StringBuilder($"_apis/projects?api-version=2.0&$skip={skip}");

            if (take.HasValue)
            {
                builder.Append("&$top={take}");
            }

            using (var response = await _httpClient.Value.GetAsync(builder.ToString(), cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<Projects>(cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets the builds.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="skip">The skip count.</param>
        /// <param name="take">The take count.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        public async Task<Builds> GetBuildsAsync(string projectId, int skip, int? take, CancellationToken cancellationToken)
        {
            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            var builder = new StringBuilder($"{projectId}/_apis/build/builds?api-version=2.0&$skip={skip}");

            if (take.HasValue)
            {
                builder.Append("&$top={take}");
            }

            using (var response = await _httpClient.Value.GetAsync(builder.ToString(), cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<Builds>(cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets the changes.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="buildId">The build identifier.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        public async Task<Changes> GetChangesAsync(string projectId, string buildId, CancellationToken cancellationToken)
        {
            Ensure.That(projectId).IsNotNullOrWhiteSpace();
            Ensure.That(buildId).IsNotNullOrWhiteSpace();

            using (var response = await _httpClient.Value.GetAsync($"{projectId}/_apis/build/builds/{buildId}/changes?api-version=2.0", cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<Changes>(cancellationToken).ConfigureAwait(false);
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
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{string.Empty}:{token}"));

            var client = new HttpClient { BaseAddress = baseUri };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            return client;
        }
    }
}