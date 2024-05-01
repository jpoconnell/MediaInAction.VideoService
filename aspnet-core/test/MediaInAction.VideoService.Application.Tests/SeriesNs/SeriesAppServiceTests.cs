using Shouldly;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.Samples;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IIdentityUserAppService here).
 * Only test your own application services.
 */
public abstract class SeriesAppServiceTests<TStartupModule> : VideoServiceApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly ISeriesAppService _seriesAppService;

    protected SeriesAppServiceTests()
    {
        _seriesAppService = GetRequiredService<ISeriesAppService>();
    }

    [Fact]
    public async Task Initial_Data_Should_Contain_Series()
    {
        //Act
        var result = await _seriesAppService.GetListPagedAsync(new PagedAndSortedResultRequestDto());

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);

    }
}
