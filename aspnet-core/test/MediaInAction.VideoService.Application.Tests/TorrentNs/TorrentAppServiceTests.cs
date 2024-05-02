using System.Threading.Tasks;
using MediaInAction.VideoService.TorrentNs.Dtos;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.TorrentNs;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IIdentityUserAppService here).
 * Only test your own application services.
 */
public abstract class TorrentAppServiceTests<TStartupModule> : VideoServiceApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly ITorrentAppService _torrentAppService;

    protected TorrentAppServiceTests()
    {
        _torrentAppService = GetRequiredService<ITorrentAppService>();
    }

    [Fact]
    public async Task Initial_Data_Should_Contain_Torrent()
    {
        //Act
        var filter = new GetTorrentsInput();
        var result = await _torrentAppService.GetTorrentsAsync(filter);
            
        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);
    }
}
