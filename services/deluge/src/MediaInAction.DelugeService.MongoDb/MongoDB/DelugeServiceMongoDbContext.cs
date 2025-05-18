using MediaInAction.DelugeService.DelugeTorrentsNs;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MediaInAction.DelugeService.MongoDB
{
    [ConnectionStringName(DelugeServiceDbProperties.ConnectionStringName)]
    public class DelugeServiceMongoDbContext : AbpMongoDbContext, IDelugeServiceMongoDbContext
    {
        public IMongoCollection<DelugeTorrent> DelugeTorrents => Collection<DelugeTorrent>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureDelugeService();
        }
    }
}