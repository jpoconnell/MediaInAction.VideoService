using MediaInAction.TraktService.MongoDb;
using Volo.Abp.Modularity;

namespace MediaInAction.TraktService
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(TraktServiceMongoDbTestModule)
        )]
    public class TraktServiceDomainTestModule : AbpModule
    {
        
    }
}
