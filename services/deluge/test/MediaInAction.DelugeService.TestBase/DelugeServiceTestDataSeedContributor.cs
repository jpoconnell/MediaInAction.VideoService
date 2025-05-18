using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.DelugeService.DelugeTorrentsNs;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.DelugeService
{
    public class DelugeServiceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly DelugeTorrentManager _delugeTorrentManager;
        private readonly TestData _testData;
        
        public DelugeServiceTestDataSeedContributor(
            DelugeTorrentManager delugeTorrentManager,
            TestData testData)
        {
            _delugeTorrentManager = delugeTorrentManager;
            _testData = testData;
        }
        
        public async Task SeedAsync(DataSeedContext context)
        {
            /* Seed additional test data... */
            await SeedTestDelugeServiceAsync();
            return ;
        }


        private async Task SeedTestDelugeServiceAsync()
        {
            //trak Torrent
            var delugeTorrent1 = new DelugeTorrentCreateDto
            {
                Hash = _testData.Hash1,
                Name = _testData.TorrentName1,
            };
            var dbTorrent1 = await _delugeTorrentManager.CreateAsync(delugeTorrent1);
            
            var delugeTorrent2 = new DelugeTorrentCreateDto
            {
                Hash = _testData.Hash2,
                Name = _testData.TorrentName2,
            };
            var dbTorrent2 = await _delugeTorrentManager.CreateAsync(delugeTorrent2);
            
        }
        
    }
}