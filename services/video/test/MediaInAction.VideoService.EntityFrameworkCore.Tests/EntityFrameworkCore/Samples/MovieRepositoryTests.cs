using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.MovieNs;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Samples;

/* This is just an example test class.
 * Normally, you don't test ABP framework code
 * Only test your custom repository methods.
 */
[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class MovieRepositoryTests : VideoServiceEntityFrameworkCoreTestBase
{
    private readonly IMovieRepository _movieRepository;

    public MovieRepositoryTests()
    {
        _movieRepository = GetRequiredService<IMovieRepository>();
    }

    [Fact]
    public async Task ShouldGetAllMoviesList()
    {
        /* Need to manually start Unit Of Work because
         * FirstOrDefaultAsync should be executed while db connection / context is available.
         */
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var movieList = await _movieRepository.GetAllListAsync();

            //Assert
            movieList.Count.ShouldBeGreaterThan(1);
        });
    }  
}
