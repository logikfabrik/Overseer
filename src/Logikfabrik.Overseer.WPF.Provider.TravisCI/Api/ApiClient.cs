// <copyright file="ApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
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
        private readonly string _gitHubToken;

        private Lazy<HttpClient> _httpClient;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public ApiClient(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            _gitHubToken = settings.GitHubToken;

            _httpClient = new Lazy<HttpClient>(() => GetHttpClient(UriUtility.GetBaseUri()));
        }

        public async Task<Accounts> GetAccountsAsync(CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed(_isDisposed);

            cancellationToken.ThrowIfCancellationRequested();

            const string url = "accounts";

            if (!HasAccessToken(_httpClient.Value))
            {
                await SetAccessTokenAsync(_httpClient.Value, _gitHubToken, cancellationToken).ConfigureAwait(false);
            }

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await SetAccessTokenAsync(_httpClient.Value, _gitHubToken, cancellationToken).ConfigureAwait(false);

                    // TODO: Handle infinite loops.
                    return await GetAccountsAsync(cancellationToken).ConfigureAwait(false);
                }

                response.ThrowIfUnsuccessful();

                return await response.Content.ReadAsAsync<Accounts>(cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task<object> GetRepositoriesAsync(string gitHubLogin, CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(gitHubLogin).IsNotNullOrWhiteSpace();

            cancellationToken.ThrowIfCancellationRequested();

            var url = $"repos?member={gitHubLogin}";

            if (!HasAccessToken(_httpClient.Value))
            {
                await SetAccessTokenAsync(_httpClient.Value, _gitHubToken, cancellationToken).ConfigureAwait(false);
            }

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await SetAccessTokenAsync(_httpClient.Value, _gitHubToken, cancellationToken).ConfigureAwait(false);

                    // TODO: Handle infinite loops.
                    return await GetRepositoriesAsync(gitHubLogin, cancellationToken).ConfigureAwait(false);
                }

                response.ThrowIfUnsuccessful();

                return await response.Content.ReadAsAsync<Accounts>(cancellationToken).ConfigureAwait(false);
            }


            

            
        }

        public async Task<object> GetBuildsAsync(string repositoryId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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

        private static bool HasAccessToken(HttpClient client)
        {
            return client.DefaultRequestHeaders.Authorization != null;
        }

        private static async Task SetAccessTokenAsync(HttpClient client, string gitHubToken, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var accessToken = await GetAccessTokenAsync(client, gitHubToken, cancellationToken).ConfigureAwait(false);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", accessToken.Token);
        }

        private static async Task<AccessToken> GetAccessTokenAsync(HttpClient httpClient, string gitHubToken, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            const string url = "auth/github";

            httpClient.DefaultRequestHeaders.Authorization = null;

            using (var response = await httpClient.PostAsync(url, new ObjectContent(typeof(Authorization), new Authorization { GitHubToken = gitHubToken }, new JsonMediaTypeFormatter()), cancellationToken).ConfigureAwait(false))
            {
                response.ThrowIfUnsuccessful();

                return await response.Content.ReadAsAsync<AccessToken>(cancellationToken).ConfigureAwait(false);
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
            // TODO: Get product name and version.
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Overseer", "1.0.0"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.travis-ci.2+json"));
        }
    }
}