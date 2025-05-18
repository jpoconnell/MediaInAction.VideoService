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
            var result = await _episodeManager.CreateUpdateAsync(episodeCreateDto);

        });

        var dbEpisode = await _episodeRepository.FindAsync(x => x.SeriesId == series.Id &&
           x.SeasonNum == episodeCreateDto.SeasonNum && x.EpisodeNum == episodeCreateDto.SeasonNum  );
        dbEpisode.EpisodeNum.ShouldBeGreaterThan(0);

    }
    
    [Fact]
    public async Task ShouldUpdateEpisode()
    {
        var series = await _seriesRepository.FindBySeriesNameYear(_testData.SeriesName1, 
            _testData.SeriesYear1);

        var airedDate =  Convert.ToDateTime("4/1/2020");
        var episodeCreateDto = new EpisodeCreateDto();
        episodeCreateDto.SeriesId = series.Id;
        episodeCreateDto.SeasonNum = 1;
        episodeCreateDto.EpisodeNum = 1;
        episodeCreateDto.AiredDate = airedDate;
        
        var episode = await _episodeRepository.FindAsync(x => x.SeriesId == series.Id 
            && x.EpisodeNum == episodeCreateDto.EpisodeNum 
            && x.SeasonNum == episodeCreateDto.SeasonNum);

        
        await WithUnitOfWorkAsync(async () =>
        {
            var result = await _episodeManager.CreateUpdateAsync(episodeCreateDto);

        });

        var dbEpisode = await _episodeRepository.FindAsync(x => x.SeriesId == series.Id 
                                                                && x.SeasonNum == episodeCreateDto.SeasonNum &&
                                                                x.EpisodeNum == episodeCreateDto.EpisodeNum );
        dbEpisode.EpisodeNum.ShouldBeGreaterThan(0);
        dbEpisode.AiredDate.ShouldBeEquivalentTo(airedDate);
    }
}
