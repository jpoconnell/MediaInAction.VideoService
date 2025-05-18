using MediaInAction.EmbyService.EmbyEpisodeNs;
using MediaInAction.EmbyService.EmbyMovieNs;
using MediaInAction.EmbyService.EmbyRequestsNs;
using MediaInAction.EmbyService.EmbyShowNs;
using MediaInAction.EmbyService.EmbyShowsNs;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MediaInAction.EmbyService.MongoDB
{
    [ConnectionStringName(EmbyServiceDbProperties.ConnectionStringName)]
    public class EmbyServiceMongoDbContext : AbpMongoDbContext, IEmbyServiceMongoDbContext
    {
        public IMongoCollection<EmbyEpisode> EmbyEpisodes => Collection<EmbyEpisode>();
        public IMongoCollection<EmbyMovie> EmbyMovies => Collection<EmbyMovie>();
        public IMongoCollection<EmbyRequest> EmbyRequests => Collection<EmbyRequest>();
        public IMongoCollection<EmbyShow> EmbyShows => Collection<EmbyShow>();
        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureEmbyService();
        }
    }
}