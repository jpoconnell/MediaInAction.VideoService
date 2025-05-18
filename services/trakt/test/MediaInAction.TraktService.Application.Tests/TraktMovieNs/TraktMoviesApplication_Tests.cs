using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktMovieNs.Dtos;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace MediaInAction.TraktService.TraktMovieNs;

public class TraktMoviesApplication_Tests : TraktServiceApplicationTestBase
{
    private readonly ITraktMovieAppService _traktMovieAppService;
    private readonly TestData _testData;
    private ICurrentUser _currentUser;

    public TraktMoviesApplication_Tests()
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

        var dmMovie = await _traktMovieAppService.CreateAsync(newMovie);
        dmMovie.ShouldNotBeNull();
    }
    
    /*
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
            Status = TraktMovieStatus.New,
            TraktMovieCreateAliases = traktMovieAliasCreateList
        };
        var dbMovie =await _traktMovieAppService.CreateAsync(traktMovieCreate);      
        dbMovie.ShouldBeNull();
    }
    */

    [Fact]
    public async Task Should_GetDashboardData()
    {
        var filter = new MovieDashboardInput();
        var dash  = await _traktMovieAppService.GetDashboardAsync(filter);
        dash.ShouldNotBeNull();
        dash.TraktMovieStatusDto.Count.ShouldBe(2);
    }
}

