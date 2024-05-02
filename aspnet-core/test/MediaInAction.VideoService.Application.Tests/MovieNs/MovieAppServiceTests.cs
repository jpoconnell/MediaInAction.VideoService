using System.Threading.Tasks;
using MediaInAction.VideoService.MovieNs.Dtos;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.MovieNs;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IIdentityUserAppService here).
 * Only test your own application services.
 */
public abstract class MovieAppServiceTests<TStartupModule> : VideoServiceApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IMovieAppService _movieAppService;

    protected MovieAppServiceTests()
    {
        _movieAppService = GetRequiredService<IMovieAppService>();
    }

    [Fact]
    public async Task Initial_Data_Should_Contain_Movie()
    {
        //Act
        var filter = new GetMoviesInput();
        var result = await _movieAppService.GetListPagedAsync(filter);

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);

    }
}
