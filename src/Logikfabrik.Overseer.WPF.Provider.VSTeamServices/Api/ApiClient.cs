// <copyright file="ApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Api
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using EnsureThat;
    using Models;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="ApiClient" /> class.
    /// </summary>
    public class ApiClient : IDisposable
    {
        private readonly Lazy<HttpClient> _httpClient;
        private bool _isDisposed;

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
        /// Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="token">The token.</param>
        /// <param name="proxyUrl">The proxy URL.</param>
        /// <param name="proxyUsername">The proxy username.</param>
        /// <param name="proxyPassword">The proxy password.</param>
        public ApiClient(string url, string token, string proxyUrl, string proxyUsername, string proxyPassword)
        {
            Ensure.That(url).IsNotNullOrWhiteSpace();
            Ensure.That(token).IsNotNullOrWhiteSpace();
            Ensure.That(proxyUrl).IsNotNullOrWhiteSpace();

            // TODO: Support for proxy.
            _httpClient = new Lazy<HttpClient>(() => GetHttpClient(new Uri(url), token, new Uri(proxyUrl), proxyUsername, proxyPassword));
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="skip">The skip count.</param>
        /// <param name="take">The take count.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        public async Task<Projects> GetProjectsAsync(int skip, int take, CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(skip).IsInRange(0, int.MaxValue);
            Ensure.That(take).IsInRange(1, int.MaxValue);

            var url = $"_apis/projects?api-version=2.0&$skip={skip}&$top={take}";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
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
        public async Task<Builds> GetBuildsAsync(string projectId, int skip, int take, CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(projectId).IsNotNullOrWhiteSpace();
            Ensure.That(skip).IsInRange(0, int.MaxValue);
            Ensure.That(take).IsInRange(1, int.MaxValue);

            var url = $"{projectId}/_apis/build/builds?api-version=2.0&$skip={skip}&$top={take}";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
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
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(projectId).IsNotNullOrWhiteSpace();
            Ensure.That(buildId).IsNotNullOrWhiteSpace();

            var url = $"{projectId}/_apis/build/builds/{buildId}/changes?api-version=2.0";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
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
            if (_isDisposed)
            {
                return;
            }

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

            _isDisposed = true;
        }

        private static HttpClient GetHttpClient(Uri baseUri, string token)
        {
            var client = new HttpClient { BaseAddress = baseUri };

            SetDefaultRequestHeaders(client, token);

            return client;
        }

        private static HttpClient GetHttpClient(Uri baseUri, string token, Uri proxyUri, string proxyUsername, string proxyPassword)
        {
            var handler = new HttpClientHandler
            {
                Proxy = new WebProxy(proxyUri),
                Credentials = new NetworkCredential { UserName = proxyUsername, Password = proxyPassword },
                UseProxy = true
            };

            var client = new HttpClient(handler) { BaseAddress = baseUri };

            SetDefaultRequestHeaders(client, token);

            return client;
        }

        private static void SetDefaultRequestHeaders(HttpClient client, string token)
        {
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{string.Empty}:{token}"));

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }
    }
}