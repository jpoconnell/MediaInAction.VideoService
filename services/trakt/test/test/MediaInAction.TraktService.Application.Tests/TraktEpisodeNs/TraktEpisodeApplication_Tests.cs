using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktShowNs;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public class TraktEpisodeApplication_Tests : TraktServiceApplicationTestBase
{
    private readonly ITraktEpisodeAppService _traktEpisodeAppService;
    private readonly ITraktShowAppService _traktShowAppService;
    private readonly TestData _testData;
    private ICurrentUser _currentUser;

    public TraktEpisodeApplication_Tests()
    {
        _testData = GetRequiredService<TestData>();
        _currentUser = GetRequiredService<ICurrentUser>();
        _traktEpisodeAppService = GetRequiredService<ITraktEpisodeAppService>();
        _traktShowAppService = GetRequiredService<ITraktShowAppService>();
    }
    
    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }
    
    [Fact]
    public async Task Should_Create_TraktEpisode()
    {
        _currentUser.Id.Returns(_testData.CurrentUserId);
        _currentUser.Email.Returns(_testData.CurrentUserEmail);
        _currentUser.Name.Returns(_testData.TestUserName);
        // Create Episode
        var newEpisode = new TraktEpisodeCreateDto();
        newEpisode.ShowSlug = _testData.ShowSlug1;
        newEpisode.SeasonNum = 1;
        newEpisode.EpisodeNum = 4;
        newEpisode.AiredDate = System.DateTime.Now;
        newEpisode.TraktEpisodeCreateAliases = new List<TraktEpisodeAliasCreateDto>();
        newEpisode.TraktEpisodeCreateAliases.Add(new TraktEpisodeAliasCreateDto()
        {
            IdType = "Test product",
            IdValue = "Code:001"
        });
        
        await _traktEpisodeAppService.CreateAsync(newEpisode).ShouldNotBeNull();      
    }
    
    
    [Fact]
    public async Task Should_Not_Create_Duplicate_TraktEpisode()
    {
        _currentUser.Id.Returns(_testData.CurrentUserId);
        _currentUser.Email.Returns(_testData.CurrentUserEmail);
        _currentUser.Name.Returns(_testData.TestUserName);
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
            ShowSlug = _testData.ShowSlug1,
            SeasonNum = _testData.SeasonNum1,
            EpisodeNum = _testData.EpisodeNum1,
            TraktEpisodeCreateAliases = traktEpisodeAliasCreateList
        };
        await _traktEpisodeAppService.CreateAsync(traktEpisodeCreate).ShouldNotBeNull();      
    }
}