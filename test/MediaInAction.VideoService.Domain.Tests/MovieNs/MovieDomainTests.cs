using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.MovieNs;

public abstract class MovieDomainTests<TStartupModule> : VideoServiceDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IMovieRepository _movieRepository;
    private readonly MovieManager _movieManager;
    private readonly TestData _testData;
    
    protected MovieDomainTests()
    {
        _movieRepository = GetRequiredService<IMovieRepository>();
        _movieManager = GetRequiredService<MovieManager>();
        _testData = GetRequiredService<TestData>();
    }

    
    [Fact]
    public async Task ShouldCreateMovie()
    {
        var movieCreateDto = new MovieCreateDto();
        var moviewName = "Test2";
        movieCreateDto.Name = moviewName;
        movieCreateDto.FirstAiredYear = _testData.MovieYear1;
     
        await WithUnitOfWorkAsync(async () =>
        {
            var result = await _movieManager.CreateAsync(movieCreateDto);

        });

        var dbMovie = await _movieRepository.FindAsync(x => x.Name ==  moviewName  );
        dbMovie.Name.ShouldBe("Test2" );
    }

    
}
