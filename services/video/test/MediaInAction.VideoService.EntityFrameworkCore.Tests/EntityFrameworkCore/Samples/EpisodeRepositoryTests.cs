using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.EpisodeNs.Dtos;
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
public class EpisodeRepositoryTests : VideoServiceEntityFrameworkCoreTestBase
{
    private readonly IEpisodeRepository _episodeRepository;

    public EpisodeRepositoryTests()
    {
        _episodeRepository = GetRequiredService<IEpisodeRepository>();
    }

    [Fact]
    public async Task ShouldGetOneEpisode()
    {
        /* Need to manually start Unit Of Work because
         * FirstOrDefaultAsync should be executed while db connection / context is available.
         */
        await WithUnitOfWorkAsync(async () =>
        {
                //Act
                var episode = await _episodeRepository
                .FirstOrDefaultAsync(u => u.SeasonNum == 1);

                //Assert
                episode.ShouldNotBeNull();
        });
    }
    
    [Fact]
    public async Task ShouldGetEpisodeList()
    {
        /* Need to manually start Unit Of Work because
         * FirstOrDefaultAsync should be executed while db connection / context is available.
         */
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var filter = "a:";
            ISpecification<Episode> specification = EpisodeNs.Specifications.SpecificationFactory.Create(filter);
            var episode = await _episodeRepository.GetMyListAsync(specification);

            //Assert
            episode.ShouldNotBeNull();
            episode.Count.ShouldBeGreaterThan(1);
        });
    }
    
    [Fact]
    public async Task ShouldGetEpisodeListBySlug()
    {
        /* Need to manually start Unit Of Work because
         * FirstOrDefaultAsync should be executed while db connection / context is available.
         */
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var filter = "a:";
            ISpecification<Episode> specification = EpisodeNs.Specifications.SpecificationFactory.Create(filter);
            var episode = await _episodeRepository.GetMyListAsync(specification);

            //Assert
            episode.ShouldNotBeNull();
            episode.Count.ShouldBeGreaterThan(1);
        });
    }
}
