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
    using JetBrains.Annotations;
    using Models;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="ApiClient" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ApiClient : IApiClient, IDisposable
    {
        private Lazy<HttpClient> _httpClient;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        [UsedImplicitly]
        public ApiClient(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            _httpClient = new Lazy<HttpClient>(() => GetHttpClient(UriUtility.BaseUri, settings.Token));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Project>> GetProjectsAsync(CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed(_isDisposed);

            cancellationToken.ThrowIfCancellationRequested();

            const string url = "api/projects";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                response.ThrowIfUnsuccessful();

                return await response.Content.ReadAsAsync<IEnumerable<Project>>(cancellationToken).ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
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
                response.ThrowIfUnsuccessful();

                return await response.Content.ReadAsAsync<ProjectHistory>(cancellationToken).ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
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

            if (disposing && _httpClient != null)
            {
                if (_httpClient.IsValueCreated)
                {
                    _httpClient.Value.Dispose();
                }

                _httpClient = null;
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