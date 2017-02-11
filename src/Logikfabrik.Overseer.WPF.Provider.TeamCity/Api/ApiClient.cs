// <copyright file="ApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Api
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
        private bool _isDisposed;

        public ApiClient(string url, string username, string password)
        {
            Ensure.That(url).IsNotNullOrWhiteSpace();
            Ensure.That(username).IsNotNullOrWhiteSpace();
            Ensure.That(password).IsNotNullOrWhiteSpace();

            _httpClient = new Lazy<HttpClient>(() => GetHttpClient(new Uri(url), username, password));
        }

        public ApiClient(string url, string username, string password, string proxyUrl, string proxyUsername, string proxyPassword)
        {
            Ensure.That(url).IsNotNullOrWhiteSpace();
            Ensure.That(username).IsNotNullOrWhiteSpace();
            Ensure.That(password).IsNotNullOrWhiteSpace();

            // TODO: Support for proxy.
            throw new NotImplementedException();
        }

        public ApiClient(string url)
        {
            Ensure.That(url).IsNotNullOrWhiteSpace();

            // TODO: Support for guest.
            throw new NotImplementedException();
        }

        public ApiClient(string url, string proxyUrl, string proxyUsername, string proxyPassword)
        {
            Ensure.That(url).IsNotNullOrWhiteSpace();

            // TODO: Support for guest with proxy.
            throw new NotImplementedException();
        }

        public async Task<Projects> GetProjets(CancellationToken cancellationToken)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            using (var response = await _httpClient.Value.GetAsync("projects", cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<Projects>(cancellationToken).ConfigureAwait(false);
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

        private static HttpClient GetHttpClient(Uri baseUri, string username, string password)
        {
            var client = new HttpClient { BaseAddress = baseUri };

            SetDefaultRequestHeaders(client, username, password);

            return client;
        }

        private static void SetDefaultRequestHeaders(HttpClient client, string username, string password)
        {
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }
    }
}