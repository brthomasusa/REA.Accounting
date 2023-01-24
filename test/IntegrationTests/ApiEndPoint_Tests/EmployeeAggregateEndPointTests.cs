#pragma warning disable CS8600, CS8602
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;

using REA.Accounting.Application.HumanResources.CreateEmployee;
using REA.Accounting.Application.HumanResources.GetEmployeeById;
using REA.Accounting.IntegrationTests.Base;

namespace REA.Accounting.IntegrationTests.ApiEndPoint_Tests
{
    public class EmployeeAggregateEndPointTests : IntegrationTest
    {
        public EmployeeAggregateEndPointTests(ApiWebApplicationFactory fixture) : base(fixture)
        { }

        [Fact]
        public async Task Get_GetEmployeeByIdQuery_ShouldSucceed()
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
        public async Task ShouldCreate_Employee_CreateEmployeeInfo_FromStream()
        {
            string uri = $"{_urlRoot}employees/create";
            CreateEmployeeCommand command = GetCreateEmployeeCommand();

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



        private CreateEmployeeCommand GetCreateEmployeeCommand()
            => new CreateEmployeeCommand
            (
                EmployeeID: 0,
                PersonType: "EM",
                NameStyle: 0,
                Title: "Mr.",
                FirstName: "Johnny",
                LastName: "Doe",
                MiddleName: "J",
                Suffix: null,
                NationalID: "13232145",
                Login: @"adventure-works\johnny0",
                JobTitle: "The Man",
                BirthDate: new DateTime(2000, 1, 28),
                MaritalStatus: "M",
                Gender: "M",
                HireDate: new DateTime(2020, 1, 28),
                Salaried: true,
                Vacation: 5,
                SickLeave: 1,
                Active: true,
                PayRate: 20.00M,
                PayFrequency: 1,
                DepartmentID: 1,
                ShiftID: 1,
                AddressType: 2,
                AddressLine1: "123 street",
                AddressLine2: "Apt 123",
                City: "Somewhere",
                StateCode: 73,
                PostalCode: "12345",
                EmailAddress: "johnny@adventure-works.com",
                PhoneNumber: "555-555-5555",
                PhoneNumberType: 2
            );
    }
}