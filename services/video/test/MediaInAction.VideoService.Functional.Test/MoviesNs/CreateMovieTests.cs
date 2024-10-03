using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using MediaInAction.VideoService.Functional.Test.Abstractions;

namespace MediaInAction.VideoService.Functional.Test.MoviesNs;

public class CreateMovieTests : BaseFunctionalTest
{
    public CreateMovieTests(FunctionalWebApplicationFactory factory) : 
        base(factory)
    {
    }

    [Fact]
    public async Task Should_BadRequest_WhenNameMissing()
    {
        // Arrange
        var request = new CreateSeriesRequest("", "name", true);
        
        // Act
        HttpResponseMessage responseMessage = await HttpClient.PostAsJsonAsync("/api/users", request);
        
        // Assert
        responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_BadRequest_WhenYearMissing()
    {
        // Arrange
        var request = new CreateSeriesRequest("email", "", true);

        // Act

        // Assert

    }
}

public class CreateSeriesRequest
{
    public CreateSeriesRequest(string empty, string name, bool b)
    {
        
        throw new NotImplementedException();
    }
}