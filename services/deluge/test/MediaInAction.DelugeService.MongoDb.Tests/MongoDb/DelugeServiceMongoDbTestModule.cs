using System;
using MediaInAction.DelugeService.MongoDB;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace MediaInAction.DelugeService.MongoDb
{
    [DependsOn(
        typeof(DelugeServiceTestBaseModule),
        typeof(DelugeServiceMongoDbModule)
        )]
    public class DelugeServiceMongoDbTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var stringArray = DelugeServiceMongoDbFixture.ConnectionString.Split('?');
            var connectionString = stringArray[0].EnsureEndsWith('/')  +
                                       "Db_" +
                                   Guid.NewGuid().ToString("N") + "/?" + stringArray[1];

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = connectionString;
            });
        }
    }
}
