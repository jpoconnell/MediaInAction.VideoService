using System.Threading.Tasks;
using MediaInAction.DelugeService.DelugeTorrentNs;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.DelugeService.DelugeTorrentsNs
{
    public class TorrentManager_Tests : DelugeServiceDomainTestBase
    {
        private readonly DelugeTorrentManager _torrentManager;
        private readonly IDelugeTorrentRepository _torrentRepository;
        
        public TorrentManager_Tests()
        {
            _torrentManager = GetRequiredService<DelugeTorrentManager>();
            _torrentRepository = GetRequiredService<IDelugeTorrentRepository>();
        }
        
        [Fact]
        public async Task Should_Update_Name()
        {
            var myTorrent = new DelugeTorrent();
            await WithUnitOfWorkAsync(async () =>
            {
                var torrent = await _torrentRepository.FirstOrDefaultAsync();
                myTorrent = torrent;
                if (torrent != null)
                {
                    torrent.Name = "FBI";
                    await _torrentRepository.UpdateAsync(torrent);
                }
            });

            var dbShowList = await _torrentRepository.GetListAsync();
            var dbShow = dbShowList[0];
            dbShow.Name.ShouldBe("FBI");
        }
        
        [Fact]
        public async Task Should_Get_List()
        {
            var dbShowList = await _torrentRepository.GetListAsync();
            var dbShow = dbShowList[0];
            dbShow.Name.ShouldNotBeNull();
        }
    }
}
