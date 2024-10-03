namespace MediaInAction.VideoService.MovieNs;

public class MovieManagerUnitTests : VideoServiceDomainTestBase
{
    private readonly MovieManager _movieManager;

    public MovieManagerUnitTests()
    {
        _movieManager = GetRequiredService<MovieManager>();
    }

    /*
    
    [Fact]
    public async Task Should_CreateMovieAsync()
    {
        var movieItems =
            new List<(Guid movieId, string idType, string idValue)>();
        movieItems.Add((Guid.NewGuid(), "Test product", "Code:001"));
        var createdMovie = await _movieManager.CreateMovieAsync(
            "paymentMethod",
            2020,
            movieItems,
            MediaType.Movie,
            MediaStatus.New
            );
        
        createdMovie.ShouldNotBeNull();
    }
    */
}