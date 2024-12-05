using System;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.SeriesNs;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IdentityUserManager here).
 * Only test your own domain services.
 */
public abstract class SeriesDomainTests<TStartupModule> : VideoServiceDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly SeriesManager _seriesManager;

    protected SeriesDomainTests()
    {
        _seriesRepository = GetRequiredService<ISeriesRepository>();
        _seriesManager = GetRequiredService<SeriesManager>();
    }

    [Fact]
    public async Task ShouldCreateSeries()
    {
        var seriesCreate = new SeriesCreateDto();
        seriesCreate.Name = Guid.NewGuid().ToString();
        seriesCreate.FirstAiredYear = 2020;
 
        await WithUnitOfWorkAsync(async () =>
        {
            var result = await _seriesManager.CreateAsync(seriesCreate);

        });

        var dbSeries = await _seriesRepository.FindAsync(x => x.Name == seriesCreate.Name);
        dbSeries.Name.ShouldNotBeNull();
    }
    
}
