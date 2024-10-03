using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.EpisodeAliasNs;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.SeriesNs.Dtos;
using NSubstitute;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace MediaInAction.VideoService.EpisodesNs;

public class EpisodeApplication_Tests : VideoServiceApplicationTestBase
{
    private readonly IEpisodeAppService _episodeAppService;
    private readonly ISeriesAppService _seriesAppService;
    private readonly TestData _testData;
    private ICurrentUser _currentUser;

    public EpisodeApplication_Tests()
    {
        _testData = GetRequiredService<TestData>();
        _currentUser = GetRequiredService<ICurrentUser>();
        _episodeAppService = GetRequiredService<IEpisodeAppService>();
        _seriesAppService = GetRequiredService<ISeriesAppService>();
        
    }
    
    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }

    [Fact]
    public async Task Should_Create_Episode()
    {
        // Create Episode
        var filter = new GetSeriesListInput();
        var seriesDtoList = await _seriesAppService.GetSeriesListAsync(filter);
        var seriesDto = seriesDtoList[0];
        var newEpisode = new EpisodeCreateDto();
        newEpisode.SeriesId = seriesDto.Id;
        newEpisode.SeasonNum = 1;
        newEpisode.EpisodeNum = 4;
        newEpisode.AiredDate = System.DateTime.Now;
        newEpisode.EpisodeCreateAliases =
        [
            new EpisodeAliasCreateDto()
            {
                IdType = "Test product",
                IdValue = "Code:001"
            }
        ];

        await _episodeAppService.CreateAsync(newEpisode).ShouldNotBeNull();      
    }
}