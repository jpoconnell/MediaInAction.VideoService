using System;
using MediaInAction.Shared.Hosting.Microservices.DbMigrations.MongoDb;
using MediaInAction.TraktService.MongoDB;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace MediaInAction.TraktService.DbMigrations;

public class TraktServiceDatabaseMigrationChecker : PendingMongoDbMigrationsChecker<TraktServiceMongoDbContext>
{
    public TraktServiceDatabaseMigrationChecker(
        IUnitOfWorkManager unitOfWorkManager,
        IServiceProvider serviceProvider,
        ICurrentTenant currentTenant,
        IDataSeeder dataSeeder,
        IAbpDistributedLock distributedLockProvider)
        : base(
            unitOfWorkManager,
            serviceProvider,
            currentTenant,
            dataSeeder,
            distributedLockProvider,
            TraktServiceDbProperties.ConnectionStringName)
    {
    }
}