using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.EpisodeNs;

public abstract class EpisodeDomainTests<TStartupModule> : VideoServiceDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IEpisodeRepository _episodeRepository;
    private readonly EpisodeManager _episodeManager;
    private readonly ISeriesRepository _seriesRepository;
    private readonly TestData _testData;
    
    protected EpisodeDomainTests()
    {
        _episodeRepository = GetRequiredService<IEpisodeRepository>();
        _episodeManager = GetRequiredService<EpisodeManager>();
        _seriesRepository = GetRequiredService<ISeriesRepository>();
        _testData = GetRequiredService<TestData>();
    }

    
    [Fact]
    public async Task ShouldCreateEpisode()
    {
        var series = await _seriesRepository.FindBySeriesNameYear(_testData.SeriesName1, 
            _testData.SeriesYear1);

        var episodeCreateDto = new EpisodeCreateDto();
        episodeCreateDto.SeriesId = series.Id;
        episodeCreateDto.SeasonNum = 2;
        episodeCreateDto.EpisodeNum = 2;
        
        
        await WithUnitOfWorkAsync(async () =>
        {
            var result = await _episodeManager.CreateAsync(episodeCreateDto);

        });

        var dbEpisode = await _episodeRepository.FindAsync(x => x.SeriesId == series.Id &&
            x.EpisodeNum == 2 );
        dbEpisode.EpisodeNum.ShouldBeGreaterThan(0);
    }

    
}
