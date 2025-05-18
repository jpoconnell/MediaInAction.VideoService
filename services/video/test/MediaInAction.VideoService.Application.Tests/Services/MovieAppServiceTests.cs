using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.MovieNs;
using MediaInAction.VideoService.MovieNs.Dtos;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.Services;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IIdentityUserAppService here).
 * Only test your own application services.
 */
public abstract class MovieAppServiceTests<TStartupModule> : VideoServiceApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IMovieAppService _moviesAppService;

    protected MovieAppServiceTests()
    {
        _moviesAppService = GetRequiredService<IMovieAppService>();
    }

    [Fact]
    public async Task Should_Count_All()
    {
        //Act
        var input = new GetMoviesInput();
        var result = await _moviesAppService.GetMoviesAsync(input);

        //Assert
        result.Count.ShouldBeGreaterThan(0);
        //result.MovieDto.Count.ShouldBeGreaterThan(0);
        //result.Items.ShouldContain(u => u.UserName == "admin");
    }
}
