using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Json;

namespace ProjectHotel.Tests.API
{

    public class CategoryControllerTests
    {
        private readonly ITestOutputHelper Output;
        private readonly HttpClient _client;
        //Менять токен по мере утраты им акутальности!
        private readonly string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkFkbWluMTIzIiwiUm9sZUlEIjoiN2VhOWIxNmMtYTY5Ny00MzBhLThmMGEtNTE4M2UzZmRkMzcxIiwiSUQiOiI0NDhhYzliZi04ZGRkLTRlMTctODFjMi01OTgyMjE2MjAwMGUiLCJuYmYiOjE2MzcyMzkyMjQsImV4cCI6MTYzNzMyNTYyNCwiaWF0IjoxNjM3MjM5MjI0fQ.xAdoWNMqvFSYkJA8f24Lrn1NwsIKXkU50QsbkrKAwqw";
        public CategoryControllerTests(ITestOutputHelper Output)
        {
            this.Output = Output;
            var server = new TestServer(new WebHostBuilder().UseEnvironment("Development").UseStartup<Startup>());
            _client = server.CreateClient();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",Token);           
        }
        [Theory]
        [InlineData("GET")]
        public async Task CategoryGetAllTest(string Method)
        {
            //Arrange
            var requste = new HttpRequestMessage(new HttpMethod(Method), "/api/Category");

            //Act
            var response = await _client.SendAsync(requste);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
    }
}
