#pragma warning disable CS8600, CS8602

using System.Net.Http.Headers;
using System.Text.Json;

using REA.Accounting.Application.Organization.GetCompany;
using REA.Accounting.Application.Organization.UpdateCompany;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;

namespace REA.Accounting.IntegrationTests.ApiEndPoint_Tests
{
    public class CompanyEndPoint_Tests : IntegrationTest
    {
        public CompanyEndPoint_Tests(ApiWebApplicationFactory fixture) : base(fixture)
        { }

        [Fact]
        public async Task Company_GetEmployeeByIdQuery_ShouldSucceed()
        {
            const int companyId = 1;
            using var response = await _client.GetAsync($"{_urlRoot}companies/{companyId}",
                                                        HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStreamAsync();
            var company = await JsonSerializer.DeserializeAsync<GetCompanyDetailByIdResponse>(jsonResponse, _options);

            Assert.Equal("Adventure-Works Cycles", company.CompanyName);
            Assert.Equal("Adventure-Works Cycles, Inc.", company.LegalName);
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
    }
}