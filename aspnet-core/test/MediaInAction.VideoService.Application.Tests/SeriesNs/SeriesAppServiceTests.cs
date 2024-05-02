using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Moq;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.SeriesNs;

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
    public async Task Get_Always_ReturnsAllSeries()
    {
        //Act
        var filter = new GetSeriesListInput();
        var result = await _seriesAppService.GetSeriesListAsync(filter);

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);
    }
    
    [Fact]
    public async Task GetById_IfExists_ReturnsSeries()
    {
        var filter = new GetSeriesInput();
        filter.Filter = "n:Law and Order";
        var result = await _seriesAppService.GetSeriesAsync(filter);
        
        Assert.NotNull(result);
        Assert.Equal("Law and Order", result!.Name);
    }
    
    [Fact]
    public async Task GetById_IfMissing_Returns404()
    {
        var filter = new GetSeriesInput();
        filter.Filter = "n:Law and DisOrder";
        var result = await _seriesAppService.GetSeriesAsync(filter);
        result.ShouldBeNull();
    }
    
}


