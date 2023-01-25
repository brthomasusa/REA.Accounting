#pragma warning disable CS8600, CS8602
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;

using REA.Accounting.Application.HumanResources.CreateEmployee;
using REA.Accounting.Application.HumanResources.UpdateEmployee;
using REA.Accounting.Application.HumanResources.GetEmployeeById;
using REA.Accounting.IntegrationTests.Base;

namespace REA.Accounting.IntegrationTests.ApiEndPoint_Tests
{
    public class EmployeeAggregateEndPointTests : IntegrationTest
    {
        public EmployeeAggregateEndPointTests(ApiWebApplicationFactory fixture) : base(fixture)
        { }

        [Fact]
        public async Task Employee_GetEmployeeByIdQuery_ShouldSucceed()
        {
            int employeeId = 1;
            using var response = await _client.GetAsync($"{_urlRoot}employees/{employeeId}",
                                                        HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStreamAsync();
            var employee = await JsonSerializer.DeserializeAsync<GetEmployeeByIdResponse>(jsonResponse, _options);

            Assert.Equal("Ken", employee.FirstName);
            Assert.Equal("Sánchez", employee.LastName);
        }

        [Fact]
        public async Task Employee_CreateEmployeeInfo_FromStream()
        {
            string uri = $"{_urlRoot}employees/create";
            CreateEmployeeCommand command = EmployeeTestData.GetCreateEmployeeCommand();

            var memStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(memStream, command);
            memStream.Seek(0, SeekOrigin.Begin);

            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var requestContent = new StreamContent(memStream))
            {
                request.Content = requestContent;
                requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();

                    var jsonResponse = await response.Content.ReadAsStreamAsync();
                    var employeeResponse = await JsonSerializer.DeserializeAsync<GetEmployeeByIdResponse>(jsonResponse, _options);
                }
            }
        }

        [Fact]
        public async Task Employee_UpdateEmployeeInfo_FromStream()
        {
            string uri = $"{_urlRoot}employees/update";
            UpdateEmployeeCommand command = EmployeeTestData.GetUpdateEmployeeCommand();

            var memStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(memStream, command);
            memStream.Seek(0, SeekOrigin.Begin);

            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var requestContent = new StreamContent(memStream))
            {
                request.Content = requestContent;
                requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        [Fact]
        public async Task Employee_DeleteEmployeeInfo_FromStream()
        {
            int employeeId = 273;
            string uri = $"{_urlRoot}employees/delete{employeeId}";

            using var response = await _client.DeleteAsync($"{_urlRoot}employees/delete/{employeeId}");

            response.EnsureSuccessStatusCode();
        }
    }
}