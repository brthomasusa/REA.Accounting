#pragma warning disable CS8600, CS8602

using System.Text.Json;
using REA.Accounting.Shared.Models.Lookups;

namespace REA.Accounting.IntegrationTests.ApiEndPoint_Tests
{
    public class LookupsEndPoint_Tests : IntegrationTest
    {
        public LookupsEndPoint_Tests(ApiWebApplicationFactory fixture) : base(fixture)
        { }

        [Fact]
        public async Task Lookups_GetStateCodeIdForAll_ShouldSucceed()
        {
            using var response = await _client.GetAsync($"{_urlRoot}lookups/statecodes/all",
                                                        HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStreamAsync();
            var statecodes = await JsonSerializer.DeserializeAsync<List<StateCode>>(jsonResponse, _options);

            Assert.True(statecodes!.Any());
            Assert.Equal(181, statecodes.Count);
        }

        [Fact]
        public async Task Lookups_GetStateCodeIdForUSA_ShouldSucceed()
        {
            using var response = await _client.GetAsync($"{_urlRoot}lookups/statecodes/usa",
                                                        HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStreamAsync();
            var statecodes = await JsonSerializer.DeserializeAsync<List<StateCode>>(jsonResponse, _options);

            Assert.True(statecodes!.Any());
            Assert.Equal(53, statecodes.Count);
        }
    }
}