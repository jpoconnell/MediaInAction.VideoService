using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.Services;

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
    public async Task ShouldGetDashboardData()
    {
        //Act
        var dashboardInput = new DashboardInput();
        var result = await _seriesAppService.GetDashboardAsync(dashboardInput );

        //Assert
        //result.Count.ShouldBeGreaterThan(0);
       // result.TotalCount.ShouldBeGreaterThan(0);
       // result.Items.ShouldContain(u => u.UserName == "admin");
    }
    
    [Fact]
    public async Task Should_Count_All()
    {
        //Act
        var input = new GetSeriessInput();
        var result = await _seriesAppService.GetSeriessAsync(input);

        //Assert
        result.Count.ShouldBeGreaterThan(0);
        //result.MovieDto.Count.ShouldBeGreaterThan(0);
        //result.Items.ShouldContain(u => u.UserName == "admin");
    }
}
