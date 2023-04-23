#pragma warning disable CS8600, CS8602

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;

using REA.Accounting.Application.Organization.GetCompany;
using REA.Accounting.Application.Organization.GetCompanyDepartments;
using REA.Accounting.Application.Organization.GetCompanyShifts;
using REA.Accounting.Application.Organization.UpdateCompany;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.IntegrationTests.ApiEndPoint_Tests
{
    public class CompanyEndPoint_Tests : IntegrationTest
    {
        public CompanyEndPoint_Tests(ApiWebApplicationFactory fixture) : base(fixture)
        { }

        [Fact]
        public async Task Company_GetCompanyDetailsByIdQuery_ShouldSucceed()
        {
            const int companyId = 1;
            using var response = await _client.GetAsync($"{_urlRoot}companies/details/{companyId}",
                                                        HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStreamAsync();
            var company = await JsonSerializer.DeserializeAsync<GetCompanyDetailByIdResponse>(jsonResponse, _options);

            Assert.Equal("Adventure-Works Cycles", company.CompanyName);
            Assert.Equal("Adventure-Works Cycles, Inc.", company.LegalName);
        }

        [Fact]
        public async Task Company_GetCompanyCommandByIdQuery_ShouldSucceed()
        {
            const int companyId = 1;
            using var response = await _client.GetAsync($"{_urlRoot}companies/command/{companyId}",
                                                        HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStreamAsync();
            var company = await JsonSerializer.DeserializeAsync<GetCompanyCommandByIdResponse>(jsonResponse, _options);

            Assert.Equal(73, company.MailStateProvinceID);
            Assert.Equal(73, company.DeliveryStateProvinceID);
        }

        [Fact]
        public async Task Company_UpdateCompanyInfo_ValidData_ShouldSucceed()
        {
            string uri = $"{_urlRoot}companies/update";
            UpdateCompanyCommand command = CompanyTestData.GetUpdateCompanyCommandWithValidData();

            var memStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(memStream, command);
            memStream.Seek(0, SeekOrigin.Begin);

            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using var requestContent = new StreamContent(memStream);
            request.Content = requestContent;
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Company_GetCompanyDepartments_ShouldSucceed()
        {
            var pagingParams = new { PageNumber = 1, PageSize = 10 };

            var queryParams = new Dictionary<string, string?>
            {
                ["pageNumber"] = pagingParams.PageNumber.ToString(),
                ["pageSize"] = pagingParams.PageSize.ToString()
            };

            List<GetCompanyDepartmentsResponse> response = await _client
                .GetFromJsonAsync<List<GetCompanyDepartmentsResponse>>(QueryHelpers.AddQueryString($"{_urlRoot}companies/departments", queryParams));

            Assert.Equal(10, response.Count);
        }

        [Fact]
        public async Task Company_GetCompanyShifts_ShouldSucceed()
        {
            var pagingParams = new { PageNumber = 1, PageSize = 2 };

            var queryParams = new Dictionary<string, string?>
            {
                ["pageNumber"] = pagingParams.PageNumber.ToString(),
                ["pageSize"] = pagingParams.PageSize.ToString()
            };

            List<GetCompanyShiftsResponse> response = await _client
                .GetFromJsonAsync<List<GetCompanyShiftsResponse>>(QueryHelpers.AddQueryString($"{_urlRoot}companies/shifts", queryParams));

            Assert.Equal(2, response.Count);
        }
    }
}