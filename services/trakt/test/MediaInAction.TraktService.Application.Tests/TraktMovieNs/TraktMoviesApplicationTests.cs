using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace MediaInAction.TraktService.TraktMovieNs;

public class TraktMoviesApplicationTests : TraktServiceApplicationTestBase
{
    private readonly ITraktMovieAppService _traktMovieAppService;
    private readonly TestData _testData;
    private ICurrentUser _currentUser;

    public TraktMoviesApplicationTests()
    {
        _testData = GetRequiredService<TestData>();
        _currentUser = GetRequiredService<ICurrentUser>();
        _traktMovieAppService = GetRequiredService<ITraktMovieAppService>();
    }
    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }
    
    [Fact]
    public async Task Should_Create_TraktMovie()
    {
        _currentUser.Id.Returns(_testData.CurrentUserId);
        _currentUser.Email.Returns(_testData.CurrentUserEmail);
        _currentUser.Name.Returns(_testData.TestUserName);
        // Create Movie
        var newMovie = new TraktMovieCreateDto();
        newMovie.Name = _testData.MovieName4;
        newMovie.FirstAiredYear = _testData.MovieYear4;
        newMovie.TraktMovieCreateAliases = new List<TraktMovieAliasCreateDto>();
        newMovie.TraktMovieCreateAliases.Add(new TraktMovieAliasCreateDto()
        {
            IdType = "Test product",
            IdValue = "Code:001"
        });
        
        await _traktMovieAppService.CreateAsync(newMovie).ShouldNotBeNull();      
    }
    
    
    [Fact]
    public async Task Should_Not_Create_Duplicate_TraktMovie()
    {
        _currentUser.Id.Returns(_testData.CurrentUserId);
        _currentUser.Email.Returns(_testData.CurrentUserEmail);
        _currentUser.Name.Returns(_testData.TestUserName);
        // Create TraktMovie
        var traktMovieAliasCreateList = new List<TraktMovieAliasCreateDto>();
            
        var traktMovieAliasCreate= new TraktMovieAliasCreateDto
        {
            IdType = "_testData.MovieType1",
            IdValue = "_testData.MovieYear1"
        };
        traktMovieAliasCreateList.Add(traktMovieAliasCreate);
            
        var traktMovieCreate = new TraktMovieCreateDto
        {
            Name = _testData.MovieName1,
            FirstAiredYear = _testData.MovieYear1,
            Slug = _testData.MovieSlug1,
            TraktMovieCreateAliases = traktMovieAliasCreateList
        };
        await _traktMovieAppService.CreateAsync(traktMovieCreate).ShouldNotBeNull();      
    }

}
