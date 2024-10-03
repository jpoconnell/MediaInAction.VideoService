using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.EntityFrameworkCore;
using MediaInAction.VideoService.EpisodeAliasNs;
using Shouldly;
using Xunit;

namespace MediaInAction.VideoService.EpisodeNs;

public class EpisodeRepository_Tests : VideoServiceEntityFrameworkCoreTestBase
{
    private readonly TestData _testData;
    private readonly IEpisodeRepository _episodeRepository;
    private readonly IVideoServiceDbContext _dbContext;
    private readonly EpisodeManager _episodeManager;

    public EpisodeRepository_Tests()
    {
        _episodeManager = GetRequiredService<EpisodeManager>();
        _dbContext = GetRequiredService<IVideoServiceDbContext>();
        _episodeRepository = GetRequiredService<IEpisodeRepository>();
        _testData = GetRequiredService<TestData>();
    }

    [Fact]
    public async Task Should_Get_All_Episodes()
    {
        var episodes =
            await _episodeRepository.GetListAsync( );
        episodes.Count.ShouldBeGreaterThan(1);
    }
    
    [Fact]
    public async Task Should_Not_Create_Episodes_with_no_Series()
    {
        var episode3 = new EpisodeCreateDto();
        episode3.SeasonNum = 1;
        episode3.EpisodeNum = 3;
        episode3.EpisodeCreateAliases = new List<EpisodeAliasCreateDto>();
        episode3.EpisodeCreateAliases.Add(new EpisodeAliasCreateDto()
        {
            IdType = "Test name",
            IdValue = "Code:002"
        });
              
        var createdEpisode =await _episodeManager.CreateAsync(episode3);
        
        createdEpisode.ShouldBeNull();
    }
}