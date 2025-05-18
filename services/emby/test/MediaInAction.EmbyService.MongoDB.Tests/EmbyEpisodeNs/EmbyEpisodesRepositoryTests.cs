using System.Threading.Tasks;
using MediaInAction.EmbyService.MongoDB;
using Shouldly;
using Xunit;

namespace MediaInAction.EmbyService.EmbyEpisodeNs;


[Collection(EmbyServiceTestConsts.CollectionDefinitionName)]
public class EmbyEpisodesRepositoryTests : EmbyServiceMongoDbTestBase
{
    private readonly IEmbyEpisodeRepository _embyEpisodeRepository;


    public EmbyEpisodesRepositoryTests()
    {
        _embyEpisodeRepository = GetRequiredService<IEmbyEpisodeRepository >();
    }
    
    
    [Fact]
    public async Task Should_Get_ListOfEpisodes()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var embyEpisodeList = await _embyEpisodeRepository.GetListAllAsync();

            //Assert
            embyEpisodeList.Count.ShouldBeGreaterThan(0);
        });
    }
}
