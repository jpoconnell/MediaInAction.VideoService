using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs;
using Shouldly;
using Volo.Abp.Specifications;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Samples;


[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class SeriesRepositoryTests : VideoServiceEntityFrameworkCoreTestBase
{
    private readonly ISeriesRepository _seriesRepository;

    public SeriesRepositoryTests()
    {
        _seriesRepository = GetRequiredService<ISeriesRepository>();
    }

  
    [Fact]
    public async Task ShouldGetSeriesList()
    {
        /* Need to manually start Unit Of Work because
         * FirstOrDefaultAsync should be executed while db connection / context is available.
         */
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var filter = "a:";
            ISpecification<Series> specification = SeriesNs.Specifications.SpecificationFactory.Create(filter);
            var series = await _seriesRepository.GetSeriessAsync(specification);

            //Assert
            series.ShouldNotBeNull();
            series.Count.ShouldBeGreaterThan(1);
        });
    }  
}
