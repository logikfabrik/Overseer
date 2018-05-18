// <copyright file="ApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Reflection;
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

            _httpClient = new Lazy<HttpClient>(() => GetHttpClient(new Uri(settings.Url), settings.Token));
        }

        /// <inheritdoc />
        public async Task<Repositories> GetRepositoriesAsync(int limit, int offset, CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(limit).IsInRange(1, int.MaxValue);
            Ensure.That(offset).IsInRange(0, int.MaxValue);

            cancellationToken.ThrowIfCancellationRequested();

            var url = $"repos?limit={limit}&offset={offset}&repository.active=true";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                response.ThrowIfUnsuccessful();

                return await response.Content.ReadAsAsync<Repositories>(cancellationToken).ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public async Task<Builds> GetBuildsAsync(string repositoryId, int limit, int offset, CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(repositoryId).IsNotNullOrWhiteSpace();
            Ensure.That(limit).IsInRange(1, int.MaxValue);
            Ensure.That(offset).IsInRange(0, int.MaxValue);

            cancellationToken.ThrowIfCancellationRequested();

            var url = $"repo/{repositoryId}/builds?limit={limit}&offset={offset}&sort_by=started_at&include=build.commit";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                response.ThrowIfUnsuccessful();

                return await response.Content.ReadAsAsync<Builds>(cancellationToken).ConfigureAwait(false);
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
            var productName = typeof(ApiClient).Assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
            var productVersion = typeof(ApiClient).Assembly.GetCustomAttribute<AssemblyVersionAttribute>()?.Version;

            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(productName, productVersion));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Travis-API-Version", new[] { "3" });
        }

        private static void SetAuthRequestHeaders(HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);
        }
    }
}