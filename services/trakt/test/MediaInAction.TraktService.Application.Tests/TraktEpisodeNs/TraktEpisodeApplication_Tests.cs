using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktEpisodeNs.Dtos;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public class TraktEpisodeApplication_Tests : TraktServiceApplicationTestBase
{
    private readonly ITraktEpisodeAppService _traktEpisodeAppService;
    private readonly TestData _testData;
    private ICurrentUser _currentUser;

    public TraktEpisodeApplication_Tests()
    {
        _testData = GetRequiredService<TestData>();
        _currentUser = GetRequiredService<ICurrentUser>();
        _traktEpisodeAppService = GetRequiredService<ITraktEpisodeAppService>();
    }
    
    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }
    
    /*
    [Fact]
    public async Task ShouldNot_Create_DuplicateTraktEpisode()
    {
        // Create Episode
        var newEpisode = new TraktEpisodeCreateDto();
        newEpisode.Slug = _testData.Slug1;
        newEpisode.SeasonNum = 1;
        newEpisode.EpisodeNum = 2;
        newEpisode.AiredDate = System.DateTime.Now;
        newEpisode.TraktEpisodeCreateAliases = new List<TraktEpisodeAliasCreateDto>();
        newEpisode.TraktEpisodeCreateAliases.Add(new TraktEpisodeAliasCreateDto()
        {
            IdType = "Test product",
            IdValue = "Code:001"
        });
        
        var dbEpisode = await _traktEpisodeAppService.CreateAsync(newEpisode);   
        dbEpisode.ShouldBeNull();
    }
    */
    
    [Fact]
    public async Task Should_Create_TraktEpisode()
    {
        // Create TraktEpisode
        var traktEpisodeAliasCreateList = new List<TraktEpisodeAliasCreateDto>();
            
        var traktEpisodeAliasCreate= new TraktEpisodeAliasCreateDto
        {
            IdType = "_testData.EpisodeType1",
            IdValue = "_testData.EpisodeYear1"
        };
        traktEpisodeAliasCreateList.Add(traktEpisodeAliasCreate);
            
        var traktEpisodeCreate = new TraktEpisodeCreateDto
        {
            Slug = _testData.Slug1,
            SeasonNum = _testData.SeasonNum1,
            EpisodeNum = _testData.EpisodeNum1,
            TraktEpisodeCreateAliases = traktEpisodeAliasCreateList
        };
        await _traktEpisodeAppService.CreateAsync(traktEpisodeCreate).ShouldNotBeNull();      
    }
    
    [Fact]
    public async Task Should_GetDashboardData()
    {
        var filter = new EpisodeDashboardInput();
        var dash = await _traktEpisodeAppService.GetDashboardAsync(filter);
        dash.ShouldNotBeNull();
        dash.TraktEpisodeStatusDto.Count.ShouldBe(2);
    }

}