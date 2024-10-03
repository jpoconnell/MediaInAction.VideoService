using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.MovieNs;


public class MovieManagerUnitTests<TStartupModule> : VideoServiceDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly MovieManager _movieManager;

    public MovieManagerUnitTests()
    {
        _movieManager = GetRequiredService<MovieManager>();
    }


    [Fact]
    public async Task Should_CreateMovieAsync()
    {
        
        await WithUnitOfWorkAsync(async () =>
        {
            var movieAlias = new MovieAliasCreateDto
            {
                IdType = "paymentMethod",
                IdValue = "Code:001"
            };

            var movieCreateDto = new MovieCreateDto
            {
                Name = "paymentMethod",
                FirstAiredYear = 2020,
                MediaStatus = MediaStatus.New
            };
            movieCreateDto.MovieAliases.Add(movieAlias);
            var createdMovie = await _movieManager.CreateAsync(movieCreateDto);
            
            createdMovie.ShouldNotBeNull();
        });
       
    }
}