using System;
using System.Threading.Tasks;
using MediaInAction.DelugeService.DelugeTorrentsNs;
using MediaInAction.DelugeService.MongoDB;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.DelugeService.TorrentNs;


[Collection(DelugeServiceTestConsts.CollectionDefinitionName)]
public class DelugeTorrentRepositoryTests : DelugeServiceMongoDbTestBase
{
    private readonly IRepository<DelugeTorrent, Guid> _traktTorrentRepository;


    public DelugeTorrentRepositoryTests()
    {
        _traktTorrentRepository = GetRequiredService<IRepository<DelugeTorrent, Guid> >();
    }

    [Fact]
    public async Task Should_Get_FirstTorrent()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var traktTorrent = await _traktTorrentRepository.FirstOrDefaultAsync();

            //Assert
            traktTorrent.ShouldNotBeNull();
        });
    }
    
    [Fact]
    public async Task Should_Get_ListOfTorrents()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var traktTorrentList = await _traktTorrentRepository.GetListAsync();

            //Assert
            traktTorrentList.Count.ShouldBeGreaterThan(0);
        });
    }
}
