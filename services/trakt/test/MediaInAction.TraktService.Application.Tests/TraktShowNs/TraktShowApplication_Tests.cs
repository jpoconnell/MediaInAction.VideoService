using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Volo.Abp.Guids;
using Volo.Abp.Users;
using Xunit;

namespace MediaInAction.TraktService.TraktShowNs;

public class TraktShowApplication_Tests : TraktServiceApplicationTestBase
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly ITraktShowAppService _traktShowAppService;
    private readonly TestData _testData;
    private ICurrentUser _currentUser;

    public TraktShowApplication_Tests()
    {
        _testData = GetRequiredService<TestData>();
        _currentUser = GetRequiredService<ICurrentUser>();
        _traktShowAppService = GetRequiredService<ITraktShowAppService>();
       _guidGenerator = GetRequiredService<IGuidGenerator>();
    }
    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }
    
    [Fact]
    public async Task Should_Create_TraktShow()
    {
        _currentUser.Id.Returns(_testData.CurrentUserId);
        _currentUser.Email.Returns(_testData.CurrentUserEmail);
        _currentUser.Name.Returns(_testData.TestUserName);
        // Create TraktShow
        var traktShowAliasCreateList = new List<TraktShowAliasCreateDto>();
        var traktShowAliasCreate= new TraktShowAliasCreateDto
        {
            IdType = "_testData.ShowType2",
            IdValue = "_testData.ShowYear2"
        };
        traktShowAliasCreateList.Add(traktShowAliasCreate);
            
        var traktShowCreate = new TraktShowCreateDto
        {
            Name = _testData.ShowName2,
            FirstAiredYear = _testData.ShowYear2,
            Slug = _testData.ShowSlug2,
            TraktShowCreateAliases = traktShowAliasCreateList
        };
        await _traktShowAppService.CreateAsync(traktShowCreate).ShouldNotBeNull();      
    }
    
    [Fact]
    public async Task Should_Not_Create_Duplicate_TraktShow()
    {
        _currentUser.Id.Returns(_testData.CurrentUserId);
        _currentUser.Email.Returns(_testData.CurrentUserEmail);
        _currentUser.Name.Returns(_testData.TestUserName);
        // Create TraktShow
        var traktShowAliasCreateList = new List<TraktShowAliasCreateDto>();
            
        var traktShowAliasCreate= new TraktShowAliasCreateDto
        {
            IdType = "_testData.ShowType1",
            IdValue = "_testData.ShowYear1"
        };
        traktShowAliasCreateList.Add(traktShowAliasCreate);
            
        var traktShowCreate = new TraktShowCreateDto
        {
            Name = _testData.ShowName1,
            FirstAiredYear = _testData.ShowYear1,
            Slug = _testData.ShowSlug1,
            TraktShowCreateAliases = traktShowAliasCreateList
        };
        await _traktShowAppService.CreateAsync(traktShowCreate).ShouldNotBeNull();      
    }
}