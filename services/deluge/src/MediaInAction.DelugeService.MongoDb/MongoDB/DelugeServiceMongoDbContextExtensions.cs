using MediaInAction.DelugeService.DelugeTorrentsNs;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace MediaInAction.DelugeService.MongoDB
{
    public static class DelugeServiceMongoDbContextExtensions
    {
        public static void ConfigureDelugeService(
            this IMongoModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            
            builder.Entity<DelugeTorrent>(x =>
            {
                x.CollectionName = "Torrents";
            });
        }
    }
}
