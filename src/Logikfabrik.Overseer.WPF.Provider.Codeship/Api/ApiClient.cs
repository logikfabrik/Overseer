// <copyright file="ApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.Codeship.Api
{
    using System;
    using System.Collections.Generic;
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
    public class ApiClient : IApiClient, IDisposable
    {
        private Lazy<HttpClient> _httpClient;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public ApiClient(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            _httpClient = new Lazy<HttpClient>(() => GetHttpClient(UriUtility.BaseUri));
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="perPage">Projects per page.</param>
        /// <param name="page">The current page.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        public async Task<IEnumerable<Project>> GetProjectsAsync(string organizationId, int perPage, int page, CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(organizationId).IsNotNullOrWhiteSpace();
            Ensure.That(perPage).IsInRange(0, 50);
            Ensure.That(page).IsInRange(0, int.MaxValue);

            cancellationToken.ThrowIfCancellationRequested();

            var url = $"organizations/{organizationId}/projects?per_page={perPage}&page={page}";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                response.ThrowIfUnsuccessful();

                return await response.Content.ReadAsAsync<IEnumerable<Project>>(cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets the builds.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="perPage">Projects per page.</param>
        /// <param name="page">The current page.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        public async Task<IEnumerable<Build>> GetBuildsAsync(string organizationId, string projectId, int perPage, int page, CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(organizationId).IsNotNullOrWhiteSpace();
            Ensure.That(projectId).IsNotNullOrWhiteSpace();
            Ensure.That(perPage).IsInRange(0, 50);
            Ensure.That(page).IsInRange(0, int.MaxValue);

            cancellationToken.ThrowIfCancellationRequested();

            var url = $"organizations/{organizationId}/projects/{projectId}/builds?per_page={perPage}&page={page}";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                response.ThrowIfUnsuccessful();

                return await response.Content.ReadAsAsync<IEnumerable<Build>>(cancellationToken).ConfigureAwait(false);
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

            if (disposing)
            {
                if (_httpClient != null)
                {
                    if (_httpClient.IsValueCreated)
                    {
                        _httpClient.Value.Dispose();
                    }

                    _httpClient = null;
                }
            }

            _isDisposed = true;
        }

        private async Task SetAccessToken(string username, string password, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            const string url = "auth";

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            using (var response = await _httpClient.Value.SendAsync(request, cancellationToken).ConfigureAwait(false))
            {
                response.ThrowIfUnsuccessful();

                var accessToken = await response.Content.ReadAsAsync<AccessToken>(cancellationToken).ConfigureAwait(false);

                _httpClient.Value.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", accessToken.Token);
            }
        }

        private static HttpClient GetHttpClient(Uri baseUri)
        {
            var client = new HttpClient { BaseAddress = baseUri };

            SetDefaultRequestHeaders(client);

            return client;
        }

        private static void SetDefaultRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
