﻿// <copyright file="ApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI.Api
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using EnsureThat;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Overseer.Api;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="ApiClient" /> class.
    /// </summary>
    public class ApiClient : CacheableApiClient<ConnectionSettings>, IApiClient
    {
        private readonly Lazy<HttpClient> _httpClient;
        private readonly JsonMediaTypeFormatter _mediaTypeFormatter;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public ApiClient(ConnectionSettings settings)
            : base(settings)
        {
            _mediaTypeFormatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter>
                    {
                        new IsoDateTimeConverter
                        {
                            DateTimeFormat = "yyyy-MM-dd'T'HH:mm:sszzz"
                        }
                    }
                }
            };

            var baseUri = BaseUriHelper.GetBaseUri(settings.Version);

            _httpClient = new Lazy<HttpClient>(() => GetHttpClient(baseUri, settings.Token));
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

            const string url = "projects";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<IEnumerable<Project>>(cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets the build types.
        /// </summary>
        /// <param name="projectVcsType">The project VCS type.</param>
        /// <param name="projectUsername">The project username.</param>
        /// <param name="projectName">The project name.</param>
        /// <param name="offset">The offset count.</param>
        /// <param name="limit">The limit count.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        public async Task<IEnumerable<Build>> GetBuildsAsync(string projectVcsType, string projectUsername, string projectName, int offset, int limit, CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(projectVcsType).IsNotNullOrWhiteSpace();
            Ensure.That(projectUsername).IsNotNullOrWhiteSpace();
            Ensure.That(projectName).IsNotNullOrWhiteSpace();
            Ensure.That(offset).IsInRange(0, int.MaxValue);
            Ensure.That(limit).IsInRange(1, int.MaxValue);

            cancellationToken.ThrowIfCancellationRequested();

            var url = $"project/{projectVcsType}/{projectUsername}/{projectName}?offset={offset}&limit={limit}";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<IEnumerable<Build>>(new[] { _mediaTypeFormatter }, cancellationToken).ConfigureAwait(false);
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
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{token}:"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }
    }
}