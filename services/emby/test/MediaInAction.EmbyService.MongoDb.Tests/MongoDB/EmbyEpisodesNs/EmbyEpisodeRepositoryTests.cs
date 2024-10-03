using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyEpisodeNs;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.EmbyService.MongoDB.EmbyEpisodesNs;

/* This is just an example test class.
 * Normally, you don't test ABP framework code
 * (like default AppUser repository IRepository<AppUser, Guid> here).
 * Only test your custom repository methods.
 */
[Collection(EmbyServiceTestConsts.CollectionDefinitionName)]
public class EmbyEpisodeRepositoryTests : EmbyServiceMongoDbTestBase
{
    private readonly IEmbyEpisodeRepository _embyEpisodeRepository;

    public EmbyEpisodeRepositoryTests()
    {
        _embyEpisodeRepository = GetRequiredService<IEmbyEpisodeRepository>();
    }

    [Fact]
    public async Task Should_Get_All_EmbyEpisodes()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var embyEpisode = await _embyEpisodeRepository.FirstOrDefaultAsync();
            //Assert
            embyEpisode.ShouldNotBeNull();
        });
    }
}
