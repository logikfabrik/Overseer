// <copyright file="ApiClientTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Test.Api
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using TeamCity.Api;
    using Xunit;

    public class ApiClientTest
    {
        //[Fact]
        public async Task CanGetProjects()
        {
            var username = "";
            var password = "";

            var client = new ApiClient("https://teamcity.jetbrains.com/httpAuth/app/rest/10.0/", username, password);

            try
            {
                var tmp = await client.GetProjets(CancellationToken.None);

                var k = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
