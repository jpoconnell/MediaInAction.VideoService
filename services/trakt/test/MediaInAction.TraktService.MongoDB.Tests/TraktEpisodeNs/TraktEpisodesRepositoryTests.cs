using System.Threading.Tasks;
using MediaInAction.TraktService.MongoDB;
using Shouldly;
using Xunit;

namespace MediaInAction.TraktService.TraktEpisodeNs;


[Collection(TraktServiceTestConsts.CollectionDefinitionName)]
public class TraktEpisodesRepositoryTests : TraktServiceMongoDbTestBase
{
    private readonly ITraktEpisodeRepository _traktEpisodeRepository;


    public TraktEpisodesRepositoryTests()
    {
        _traktEpisodeRepository = GetRequiredService<ITraktEpisodeRepository >();
    }
    
    
    [Fact]
    public async Task Should_Get_ListOfEpisodes()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var traktEpisodeList = await _traktEpisodeRepository.GetListAllAsync();

            //Assert
            traktEpisodeList.Count.ShouldBeGreaterThan(0);
        });
    }
}
