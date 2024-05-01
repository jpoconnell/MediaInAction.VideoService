using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.Enums;
using Shouldly;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.MovieNs;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IdentityUserManager here).
 * Only test your own domain services.
 */
public abstract class MovieManagerUnitTests<TStartupModule> : VideoServiceDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly MovieManager _movieManager;
    
    public MovieManagerUnitTests()
    {
        _movieManager = GetRequiredService<MovieManager>();
    }

    
    [Fact]
    public async Task Should_CreateMovieAsync1()
    {
        var movieItems =
            new List<( string idType, string idValue)>();
        movieItems.Add(( "Test product", "Code:001"));
        
        var createdMovie = await _movieManager.CreateAsync(
            "Clue",
            2020,
            movieItems,
            MediaType.Movie,
            MediaStatus.New
        );
        
        createdMovie.ShouldNotBeNull();
    }
}
