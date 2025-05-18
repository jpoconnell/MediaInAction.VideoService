using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.MapperNs;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.ToBeMappedNs;
using Shouldly;
using Volo.Abp.Modularity;
using Volo.Abp.Specifications;
using Xunit;

namespace MediaInAction.VideoService.Services;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IIdentityUserAppService here).
 * Only test your own application services.
 */
public abstract class ToBeMappedAppServiceTests<TStartupModule> : VideoServiceApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IMapperAppService _toBeMappedAppService;

    protected ToBeMappedAppServiceTests()
    {
        _toBeMappedAppService = GetRequiredService<IMapperAppService>();
    }

    [Fact]
    public async Task ShouldGetDashboardData()
    {
        //Act
        var filter = "a:";
        ISpecification<ToBeMappedNs.ToBeMapped> specification = ToBeMappedNs.Specifications.SpecificationFactory.Create(filter);
        var result = await _toBeMappedAppService.GetDashboardAsync(specification );

        //Assert
        result.ToBeMappedStatusDto.Count.ShouldBeGreaterThan(0);

    }
}