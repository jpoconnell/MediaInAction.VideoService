using MediaInAction.EmbyService.EmbyShowNs;
using MediaInAction.EmbyService.EmbyShowsNs;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace MediaInAction.EmbyService.MongoDB
{
    public static class EmbyServiceMongoDbContextExtensions
    {
        public static void ConfigureEmbyService(
            this IMongoModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            
            builder.Entity<EmbyShow>(x =>
            {
                x.CollectionName = "EmbyShows";
            });
        }
    }
}
