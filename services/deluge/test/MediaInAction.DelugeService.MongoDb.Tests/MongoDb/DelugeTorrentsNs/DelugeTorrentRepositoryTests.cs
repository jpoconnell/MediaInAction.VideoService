using System.Threading.Tasks;
using MediaInAction.DelugeService.DelugeTorrentNs;
using MediaInAction.DelugeService.DelugeTorrentNs.Specifications;
using MediaInAction.DelugeService.DelugeTorrentsNs;
using Shouldly;
using Volo.Abp.Specifications;
using Xunit;

namespace MediaInAction.DelugeService.MongoDb.DelugeTorrentsNs;


[Collection(DelugeServiceTestConsts.CollectionDefinitionName)]
public class DelugeTorrentRepositoryTests : DelugeServiceMongoDbTestBase
{
    private readonly IDelugeTorrentRepository _torrentRepository;
    private readonly DelugeTorrentManager _torrentManager;

    public DelugeTorrentRepositoryTests()
    {
        _torrentRepository = GetRequiredService<IDelugeTorrentRepository>();
        _torrentManager = GetRequiredService<DelugeTorrentManager>();
    }

    [Fact]
    public async Task Should_Query_torrent()
    {
        await WithUnitOfWorkAsync(async () =>
        { 
            //Act
            ISpecification<DelugeTorrent> specification = SpecificationFactory.Create("");
            
            var torrentList = await _torrentRepository.GetListPagedAsync(
                specification, 0,10,"",false);
                
            //Assert
            torrentList.ShouldNotBeNull();
        });
    }
    
    [Fact]
    public async Task Should_Create_new_torrent()
    {
        await WithUnitOfWorkAsync(async () =>
        { 
            //Act
            var newTorrent = new DelugeTorrentCreateDto
            {
                Comment = "no comment",
                Name = "ssss",
                IsSeed = false,
                Hash = "hash4",
                Paused = false,
                Ratio = 0.0,
                Message = "no message",
                Label = "label",
                Added = 2000,
                CompleteTime = 0.0,
                DownloadLocation = "/downloads"
            };

            var createdTorrent = await _torrentManager.CreateAsync(newTorrent);
            
            //Assert
            createdTorrent.ShouldNotBeNull();
        });
    }
}
