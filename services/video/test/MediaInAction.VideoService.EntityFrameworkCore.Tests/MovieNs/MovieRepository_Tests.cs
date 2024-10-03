using System.Threading.Tasks;
using MediaInAction.VideoService.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace MediaInAction.VideoService.MovieNs;

public class MovieRepository_Tests : VideoServiceEntityFrameworkCoreTestBase
{
    private readonly TestData _testData;
    private readonly IMovieRepository _moviesRepository;
    private readonly IVideoServiceDbContext _dbContext;
    private readonly MovieManager _movieManager;

    public MovieRepository_Tests()
    {
        _movieManager = GetRequiredService<MovieManager>();
        _dbContext = GetRequiredService<IVideoServiceDbContext>();
        _moviesRepository = GetRequiredService<IMovieRepository>();
        _testData = GetRequiredService<TestData>();
    }
    
    [Fact]
    public async Task Should_Get_All_Movies()
    {
        var movies =
            await _moviesRepository.GetListAsync();
        movies.Count.ShouldBe(2);
    }
}