using System.Threading.Tasks;
using MediaInAction.DelugeService.DelugeTorrentsNs;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.DelugeService.TorrentNs;

    public class DelugeTorrentManagerTests : DelugeServiceDomainTestBase
    {
        private readonly DelugeTorrentManager _embyTorrentManager;
        private readonly IDelugeTorrentRepository _embyTorrentRepository;
        
        public DelugeTorrentManagerTests()
        {
            _embyTorrentManager = GetRequiredService<DelugeTorrentManager>();
            _embyTorrentRepository = GetRequiredService<IDelugeTorrentRepository>();
        }
        
    [Fact]
    public async Task Should_Set_Name()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            var show = await _embyTorrentRepository.FirstOrDefaultAsync();
            show.Name = "FBI";
            await _embyTorrentRepository.UpdateAsync(show);
        });

        var  dbTorrentList = await _embyTorrentRepository.GetListAsync();
        var dbTorrent = dbTorrentList[0];
        dbTorrent.Name.ShouldBe("FBI");
    }

}
