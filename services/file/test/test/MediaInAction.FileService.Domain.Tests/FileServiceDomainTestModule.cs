using MediaInAction.FileService.MongoDB;
using Volo.Abp.Modularity;

namespace MediaInAction.FileService
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(FileServiceMongoDbTestModule)
        )]
    public class FileServiceDomainTestModule : AbpModule
    {
        
    }
}
