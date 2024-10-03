using System.Threading.Tasks;
using MediaInAction.VideoService.EntityFrameworkCore;
using MediaInAction.VideoService.ToBeMappedNs;
using MediaInAction.VideoService.ToBeMappedNs.Specifications;
using Shouldly;
using Xunit;

namespace MediaInAction.VideoService.ToBeMappedsNs;

public class ToBeMappedRepository_Tests : VideoServiceEntityFrameworkCoreTestBase
{
    private readonly TestData _testData;
    private readonly IToBeMappedRepository _toBeMappedRepository;
    private readonly IVideoServiceDbContext _dbContext;
    private readonly ToBeMappedManager _toBeMappedManager;

    public ToBeMappedRepository_Tests()
    {
        _toBeMappedManager = GetRequiredService<ToBeMappedManager>();
        _dbContext = GetRequiredService<IVideoServiceDbContext>();
        _toBeMappedRepository = GetRequiredService<IToBeMappedRepository>();
        _testData = GetRequiredService<TestData>();
    }

    [Fact]
    public async Task Should_Get_User_ToBeMappeds()
    {
        var toBeMapped =
            await _toBeMappedRepository.GetToBeMappedsByUserId(_testData.CurrentUserId, new Last30DaysSpecification());
        toBeMapped.Count.ShouldBe(3);
    }
    
}