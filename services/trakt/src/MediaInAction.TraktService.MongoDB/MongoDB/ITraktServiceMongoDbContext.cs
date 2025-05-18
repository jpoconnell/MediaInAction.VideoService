using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MediaInAction.TraktService.MongoDB
{
    [ConnectionStringName(TraktServiceDbProperties.ConnectionStringName)]
    public interface ITraktServiceMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
