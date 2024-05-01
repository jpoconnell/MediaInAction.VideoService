using Shouldly;
using System.Threading.Tasks;
using MediaInAction.VideoService.EpisodeNs;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.Samples;

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
        var result = await _episodeAppService.GetListPagedAsync(new PagedAndSortedResultRequestDto());

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);
    }
}
