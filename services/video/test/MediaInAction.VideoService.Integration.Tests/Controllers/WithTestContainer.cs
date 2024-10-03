using System.Net.Http.Json;
using FluentAssertions;
using MediaInAction.VideoService.Integration.Test.Fixtures;
using MediaInAction.VideoService.Integration.Test.Helper;
using MediaInAction.VideoService.SeriesNs;

namespace MediaInAction.VideoService.Integration.Test.Controllers
{
    public class WithTestContainer: IClassFixture<DockerWebApplicationFactoryFixture>
    {
        private readonly DockerWebApplicationFactoryFixture _factory;
        private readonly HttpClient _client;

        public WithTestContainer(DockerWebApplicationFactoryFixture factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task OnGetSeries_WhenExecuteController_ShouldreturnTheExpecedSeries()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync(HttpHelper.Urls.GetAllSeries);
            var result = await response.Content.ReadFromJsonAsync<List<Series>>();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            result.Count.Should().Be(_factory.InitialSeriesCount);
          //  result.Should()
            //    .BeEquivalentTo(DataFixture.GetSeries(_factory.InitialSeriesCount), options => options.Excluding(t => t.SeriesId));
        }

        [Fact]
        public async Task OnAddSeries_WhenExecuteController_ShouldStoreInDb()
        {
            // Arrange
            var newSeries = DataFixture.GetSeries(1,true);

            // Act
            var request = await _client.PostAsync(HttpHelper.Urls.AddSeries, HttpHelper.GetJsonHttpContent(newSeries));
            var response = await _client.GetAsync($"{HttpHelper.Urls.GetSeries}/{_factory.InitialSeriesCount + 1}");
            var result = await response.Content.ReadFromJsonAsync<Series>();

            // Assert
            request.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            //result.Name.Should().Be(newSeries.Name);
            //result.FirstAiredYear.Should().Be(newSeries.FirstAiredYear);
        }
    }
}
