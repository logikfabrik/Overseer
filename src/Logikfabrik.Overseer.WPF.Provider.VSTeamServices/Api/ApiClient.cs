// <copyright file="ApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Api
{
    /// <summary>
    /// The <see cref="ApiClient" /> class.
    /// </summary>
    public class ApiClient
    {
        ///*
        // * 
        // * 
        // * 
        // * 
        // * 
        // * Build (read) + Project and team (read)
        // */

        //private readonly Uri _serviceUrl;

        ////private readonly VssBasicCredential _credentials;

        //private readonly string _token;

        //public Client2(Uri serviceUrl, string token)
        //{
        //    if (serviceUrl == null)
        //    {
        //        throw new ArgumentNullException(nameof(serviceUrl));
        //    }

        //    if (string.IsNullOrWhiteSpace(token))
        //    {
        //        throw new ArgumentException("Token cannot be null or white space.", nameof(token));
        //    }

        //    _serviceUrl = serviceUrl;
        //    //_credentials = new VssBasicCredential(string.Empty, token);

        //    _token = token;
        //}

        ////public async Task<IEnumerable<Build>> GetBuilds()
        ////{
        ////    using (var client = new BuildHttpClient(_serviceUrl, _credentials))
        ////    {
        ////        return await client.GetBuildsAsync();
        ////    }
        ////}

        //public async Task<Projects> GetProjects()
        //{
        //    string credentials = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", _token)));
            
        //    //use the httpclient
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://xxx.visualstudio.com:");  //url of our account
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

        //        //connect to the REST endpoint            
        //        HttpResponseMessage response = client.GetAsync("_apis/projects?stateFilter=All&api-version=1.0").Result;

        //        response.EnsureSuccessStatusCode();
                  
        //            return await response.Content.ReadAsAsync<Projects>();
 
        //    }



        //    //using (var client = new ProjectHttpClient(_serviceUrl, _credentials))
        //    //{
        //    //    return await client.GetProjects();
        //    //}
        //}
    }
}
