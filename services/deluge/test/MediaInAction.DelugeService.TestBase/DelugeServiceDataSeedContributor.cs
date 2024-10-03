using System.Threading.Tasks;
using MediaInAction.DelugeService.DelugeTorrentsNs;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.DelugeService
{
    public class DelugeServiceDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly DelugeTorrentManager _torrentManager;
        private readonly IDelugeTorrentRepository _torrentRepository;
        private readonly TestData _testData;
        
        public DelugeServiceDataSeedContributor(
            DelugeTorrentManager torrentManager,
            IDelugeTorrentRepository torrentRepository,
            TestData testData)
        {
            _torrentManager = torrentManager;
            _torrentRepository = torrentRepository;
            _testData = testData;
        }

        public Task SeedAsync(DataSeedContext context)
        {
            SeedTestDelugeServiceAsync();
            return Task.CompletedTask;
        }

        public async Task SeedTestDelugeServiceAsync()
        {
            var torrent1 = new DelugeTorrentCreateDto
            {
                Comment = "no comment",
                Name = _testData.TorrentName1,
                Hash = "hash1",
                IsPaused = false
            };
           var new1 = await _torrentManager.CreateAsync(torrent1);
            
            var torrent2 = new DelugeTorrentCreateDto
            {
                Comment = "no comment",
                Name = "sssss",
                Hash = "hash2",
                IsPaused = false
            };

            await _torrentManager.CreateAsync(torrent2);
        }
    }
}
