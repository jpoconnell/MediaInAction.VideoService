using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.SeriesNs;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IIdentityUserAppService here).
 * Only test your own application services.
 */
public abstract class PublicSeriesAppServiceTests<TStartupModule> : VideoServiceApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IPublicSeriesAppService _publicSeriesAppService;

    protected PublicSeriesAppServiceTests()
    {
        _publicSeriesAppService = GetRequiredService<IPublicSeriesAppService>();
    }

    [Fact]
    public async Task Initial_Data_Should_Contain_Series()
    {
        //Act
        var seriesList = await _publicSeriesAppService.GetListAsync();

        //Assert
        seriesList.Count.ShouldBeGreaterThan(0);

    }
}
