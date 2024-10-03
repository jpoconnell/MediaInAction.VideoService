using MediaInAction.EmbyService.EmbyShowsNs;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MediaInAction.EmbyService.MongoDB
{
    [ConnectionStringName(EmbyServiceDbProperties.ConnectionStringName)]
    public class EmbyServiceMongoDbContext : AbpMongoDbContext, IEmbyServiceMongoDbContext
    {
        public IMongoCollection<EmbyShow> EmbyShows => Collection<EmbyShow>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureEmbyService();
        }
    }
}