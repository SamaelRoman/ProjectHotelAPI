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
using System.Text.Json;
using System.Text.Json.Serialization;
using ProjectHotel.ViewModels;

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
        private void IsAuth(HttpStatusCode responseCode)
        {
            if (responseCode == HttpStatusCode.Unauthorized)
            {
                Output.WriteLine("Прошу прощения за неудобство, поменяйте пожулуйста токен!(");
            }
        }
        [Theory]
        [InlineData("GET")]
        public async Task Category_Get_AllTest(string Method)
        {
            //Arrange
            var requste = new HttpRequestMessage(new HttpMethod(Method), "/api/Category");

            //Act
            var response = await _client.SendAsync(requste);

            //Assert
            IsAuth(response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
        [Theory]
        [InlineData("GET")]
        public async Task Category_GetByID_Test(string Method)
        {
            //Arrange
            string ID = "8e57e414-bb4b-4eb6-a9c1-cd71b9f1bc15"; //exist ID
            var requste = new HttpRequestMessage(new HttpMethod(Method),$"/api/Category/{ID}");

            //Act
            var response = await _client.SendAsync(requste);

            //Assert
            IsAuth(response.StatusCode);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var TResult = response.Content.ReadAsStringAsync();
            var Result = TResult.Result;

            var Category = JsonSerializer.Deserialize<CategoryViewModel>(Result);

            Assert.NotNull(Category);

        }
        [Theory]
        [InlineData("GET")]
        public async Task Category_GetByNotExistID_Test(string Method)
        {
            //Arrange
            string ID = "8e57e414-bb4b-4eb6-a9c1-cd71b9f11215"; //not exist ID
            var requste = new HttpRequestMessage(new HttpMethod(Method), $"/api/Category/{ID}");

            //Act
            var response = await _client.SendAsync(requste);

            //Assert
            IsAuth(response.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
