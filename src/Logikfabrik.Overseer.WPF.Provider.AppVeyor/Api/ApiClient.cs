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
    using Caching;
    using EnsureThat;
    using Models;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="ApiClient" /> class.
    /// </summary>
    public class ApiClient : IApiClient, IDisposable
    {
        private readonly string _cacheBaseKey;
        private Lazy<HttpClient> _httpClient;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public ApiClient(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            _httpClient = new Lazy<HttpClient>(() => GetHttpClient(UriUtility.BaseUri, settings.Token));
            _cacheBaseKey = string.Concat(UriUtility.BaseUri, settings.Token);
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        public async Task<IEnumerable<Project>> GetProjectsAsync(CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed(_isDisposed);

            cancellationToken.ThrowIfCancellationRequested();

            const string url = "api/projects";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
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
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(accountName).IsNotNullOrWhiteSpace();
            Ensure.That(projectSlug).IsNotNullOrWhiteSpace();
            Ensure.That(recordsNumber).IsInRange(1, int.MaxValue);

            cancellationToken.ThrowIfCancellationRequested();

            var url = $"api/projects/{accountName}/{projectSlug}/history?recordsNumber={recordsNumber}";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
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
        }

        /// <summary>
        /// Gets the cache key for this <see cref="ICacheable" /> instance.
        /// </summary>
        /// <returns>The cache key.</returns>
        public string GetCacheBaseKey()
        {
            return _cacheBaseKey;
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

        private static HttpClient GetHttpClient(Uri baseUri, string token)
        {
            var client = new HttpClient { BaseAddress = baseUri };

            SetDefaultRequestHeaders(client);
            SetAuthRequestHeaders(client, token);

            return client;
        }

        private static void SetDefaultRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static void SetAuthRequestHeaders(HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}