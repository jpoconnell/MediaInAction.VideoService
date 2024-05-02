using System.Threading.Tasks;
using MediaInAction.VideoService.ToBeMappedNs.Dtos;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.ToBeMappedNs;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IIdentityUserAppService here).
 * Only test your own application services.
 */
public abstract class ToBeMappedAppServiceTests<TStartupModule> : VideoServiceApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IToBeMappedAppService _toBeMappedAppService;

    protected ToBeMappedAppServiceTests()
    {
        _toBeMappedAppService = GetRequiredService<IToBeMappedAppService>();
    }

    [Fact]
    public async Task Initial_Data_Should_Contain_ToBeMapped()
    {
        //Act
        var filter = new GetToBeMappedsInput();
        var result = await _toBeMappedAppService.GetListPagedAsync(filter);

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);

    }
}
