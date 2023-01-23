using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Xunit;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.IntegrationTests.Base
{
    [Trait("Category", "Integration")]
    public abstract class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
    {
        protected readonly ApiWebApplicationFactory _factory;
        protected readonly HttpClient _client;
        protected readonly string _urlRoot = "api/";
        protected readonly JsonSerializerOptions _options;

        public IntegrationTest(ApiWebApplicationFactory fixture)
        {
            _factory = fixture;
            _client = _factory.CreateClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            OperationResult<bool> result = ReseedTestDatabase.ReseedDatabase();
        }
    }
}