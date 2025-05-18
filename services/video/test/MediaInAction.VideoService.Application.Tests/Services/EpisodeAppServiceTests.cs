using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.Services;

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
    public async Task ShouldGetDashboardData()
    {
        //Act
        var dashboardInput = new EpisodeDashboardInput();
        var result = await _episodeAppService.GetDashboardAsync(dashboardInput );

        //Assert
        result.EpisodeStatusDto.Count.ShouldBeGreaterThan(0);
       // result.Items.ShouldContain(u => u.UserName == "admin");
    }
}
