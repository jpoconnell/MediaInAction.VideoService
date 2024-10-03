using System.Threading.Tasks;
using MediaInAction.VideoService.EntityFrameworkCore;
using MediaInAction.VideoService.SeriesNs.Specifications;
using Xunit;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesRepository_Tests : VideoServiceEntityFrameworkCoreTestBase
{
    private readonly TestData _testData;
    private readonly ISeriesRepository _seriesRepository;
    private readonly IVideoServiceDbContext _dbContext;
    private readonly SeriesManager _seriesManager;

    public SeriesRepository_Tests()
    {
        _seriesManager = GetRequiredService<SeriesManager>();
        _dbContext = GetRequiredService<IVideoServiceDbContext>();
        _seriesRepository = GetRequiredService<ISeriesRepository>();
        _testData = GetRequiredService<TestData>();
    }
    
}