using System;
using System.Threading.Tasks;
using MediaInAction.TraktService.MongoDB;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.TraktService.TraktMovieNs;


[Collection(TraktServiceTestConsts.CollectionDefinitionName)]
public class TraktMoviesRepositoryTests : TraktServiceMongoDbTestBase
{
    private readonly IRepository<TraktMovie, Guid> _traktMovieRepository;


    public TraktMoviesRepositoryTests()
    {
        _traktMovieRepository = GetRequiredService<IRepository<TraktMovie, Guid> >();
    }

    [Fact]
    public async Task Should_Get_FirstMovie()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var traktMovie = await _traktMovieRepository.FirstOrDefaultAsync();

            //Assert
            traktMovie.ShouldNotBeNull();
        });
    }
    
    [Fact]
    public async Task Should_Get_ListOfMovies()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var traktMovieList = await _traktMovieRepository.GetListAsync();

            //Assert
            traktMovieList.Count.ShouldBeGreaterThan(0);
        });
    }
}
