using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.EpisodeAliasNs;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.SeriesNs;
using Shouldly;
using Volo.Abp.Specifications;
using Xunit;

namespace MediaInAction.VideoService.EpisodesNs;

public class EpisodeManagerUnitTests : VideoServiceDomainTestBase
{
    private readonly EpisodeManager _episodeManager;
    private readonly ISeriesRepository _seriesRepository;

    public EpisodeManagerUnitTests()
    {
        _episodeManager = GetRequiredService<EpisodeManager>();
        _seriesRepository = GetRequiredService<ISeriesRepository>();
    }
    
    [Fact]
    public async Task Should_CreateEpisodeAsync()
    {
        var filter = "" ;
        ISpecification<Series> specification = MediaInAction.VideoService.SeriesNs.Specifications.SpecificationFactory.Create(filter);
        var seriesDtoList = await _seriesRepository.GetSeriesBySpec(specification);
        
        var newEpisode = new EpisodeCreateDto();
        
        newEpisode.SeriesId = seriesDtoList[0].Id;; 
        newEpisode.SeasonNum = 2;
        newEpisode.EpisodeNum = 1;
        newEpisode.AiredDate = DateTime.Now;
        newEpisode.EpisodeCreateAliases = new List<EpisodeAliasCreateDto>();
        
        newEpisode.EpisodeCreateAliases.Add(new EpisodeAliasCreateDto()
        {
            IdType = "Test product",
            IdValue = "Code:001"
        });
        var createdEpisode = await _episodeManager.CreateAsync(newEpisode);
        createdEpisode.ShouldNotBeNull();
    }
}