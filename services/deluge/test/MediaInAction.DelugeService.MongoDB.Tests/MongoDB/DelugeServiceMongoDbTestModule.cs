using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace MediaInAction.DelugeService.MongoDB
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

            Console.WriteLine(connectionString);
            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = connectionString;
            });
        }
    }
}
