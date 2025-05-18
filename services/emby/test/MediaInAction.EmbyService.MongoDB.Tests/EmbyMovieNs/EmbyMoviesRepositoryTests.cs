using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService.MongoDB;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.EmbyService.EmbyMovieNs;


[Collection(EmbyServiceTestConsts.CollectionDefinitionName)]
public class EmbyMoviesRepositoryTests : EmbyServiceMongoDbTestBase
{
    private readonly IRepository<EmbyMovie, Guid> _embyMovieRepository;


    public EmbyMoviesRepositoryTests()
    {
        _embyMovieRepository = GetRequiredService<IRepository<EmbyMovie, Guid> >();
    }

    [Fact]
    public async Task Should_Get_FirstMovie()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var embyMovie = await _embyMovieRepository.FirstOrDefaultAsync();

            //Assert
            embyMovie.ShouldNotBeNull();
        });
    }
    
    [Fact]
    public async Task Should_Get_ListOfMovies()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var embyMovieList = await _embyMovieRepository.GetListAsync();

            //Assert
            embyMovieList.Count.ShouldBeGreaterThan(0);
        });
    }
}
