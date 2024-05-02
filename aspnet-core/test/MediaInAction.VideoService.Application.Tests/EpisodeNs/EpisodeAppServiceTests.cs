using System.Threading.Tasks;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.EpisodeNs;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IIdentityUserAppService here).
 * Only test your own application services.
 */
public abstract class EpisodeAppServiceTests<TStartupModule> : VideoServiceApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IEpisodeAppService _episodeAppService;

    protected EpisodeAppServiceTests()
    {
        _episodeAppService = GetRequiredService<IEpisodeAppService>();
    }

    [Fact]
    public async Task Initial_Data_Should_Contain_Admin_User()
    {
        //Act
        var filter = new GetMyEpisodesInput();
        var result = await _episodeAppService.GetListPagedAsync(filter);

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);
    }
}
