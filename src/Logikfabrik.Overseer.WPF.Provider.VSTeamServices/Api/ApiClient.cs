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

            var baseUri = new Uri(settings.Url);

            _httpClient = new Lazy<HttpClient>(() => GetHttpClient(baseUri, settings.Token));
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

            cancellationToken.ThrowIfCancellationRequested();

            var url = $"_apis/projects?api-version=2.0&$skip={skip}&$top={take}";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                response.ThrowIfUnsuccessful();

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

            cancellationToken.ThrowIfCancellationRequested();

            var url = $"{projectId}/_apis/build/builds?api-version=2.0&$skip={skip}&$top={take}";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                response.ThrowIfUnsuccessful();

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

            cancellationToken.ThrowIfCancellationRequested();

            var url = $"{projectId}/_apis/build/builds/{buildId}/changes?api-version=2.0";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                response.ThrowIfUnsuccessful();

                return await response.Content.ReadAsAsync<Changes>(cancellationToken).ConfigureAwait(false);
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
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{string.Empty}:{token}"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }
    }
}