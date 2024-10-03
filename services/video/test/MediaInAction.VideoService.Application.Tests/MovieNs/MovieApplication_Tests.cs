using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace MediaInAction.VideoService.MovieNs;

public class MovieApplication_Tests : VideoServiceApplicationTestBase
{
    private readonly IMovieAppService _moviesAppService;
    private readonly TestData _testData;
    private ICurrentUser _currentUser;

    public MovieApplication_Tests()
    {
        _testData = GetRequiredService<TestData>();
        _currentUser = GetRequiredService<ICurrentUser>();
        _moviesAppService = GetRequiredService<IMovieAppService>();
    }
    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }

    [Fact]
    public async Task Should_Create_Movie()
    {
        _currentUser.Id.Returns(_testData.CurrentUserId);
        _currentUser.Email.Returns(_testData.CurrentUserEmail);
        _currentUser.Name.Returns(_testData.TestUserName);
        // Create Movies
        var newMovieCreateAliasList = new List<MovieAliasCreateDto>();
        var newMovieAlias = new MovieAliasCreateDto();
        newMovieAlias.IdType = "name";
        newMovieAlias.IdValue = "paypal";
        newMovieCreateAliasList.Add(newMovieAlias);

        var newMovie = new MovieCreateDto
        {
            Name = "paypal4",
            FirstAiredYear = 2000,
            MovieAliases = newMovieCreateAliasList
        };

        var createdMovie = await _moviesAppService.CreateAsync(newMovie);
        createdMovie.ShouldNotBeNull();
    }
    
    /*
    [Fact]
    public async Task Should_Not_Create_Dulpicate_Movie()
    {
        _currentUser.Id.Returns(_testData.CurrentUserId);
        _currentUser.Email.Returns(_testData.CurrentUserEmail);
        _currentUser.Name.Returns(_testData.TestUserName);
        // Create Movies
        var newMovie = new MovieCreateDto();
        newMovie.Name = _testData.MovieName1;
        newMovie.FirstAiredYear = _testData.MovieYear1;
        newMovie.MovieAliases = new List<MovieAliasCreateDto>();
        
        newMovie.MovieAliases.Add(new MovieAliasCreateDto()
        {
            IdType = "name",
            IdValue = "paypal"
        });
        
        var createdMovie = await _moviesAppService.CreateAsync(newMovie);
        createdMovie.ShouldBeNull();
    }
    */
}