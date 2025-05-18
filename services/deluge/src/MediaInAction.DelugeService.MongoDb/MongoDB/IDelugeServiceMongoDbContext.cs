using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MediaInAction.DelugeService.MongoDB
{
    [ConnectionStringName(DelugeServiceDbProperties.ConnectionStringName)]
    public interface IDelugeServiceMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
