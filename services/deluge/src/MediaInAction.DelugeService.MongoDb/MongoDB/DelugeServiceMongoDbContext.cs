using MediaInAction.DelugeService.DelugeTorrentNs;
using MediaInAction.DelugeService.TorrentsNs;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MediaInAction.DelugeService.MongoDB
{
    [ConnectionStringName(DelugeServiceDbProperties.ConnectionStringName)]
    public class DelugeServiceMongoDbContext : AbpMongoDbContext, IDelugeServiceMongoDbContext
    {
        public IMongoCollection<DelugeTorrent> Products => Collection<DelugeTorrent>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureDelugeService();
        }
    }
}